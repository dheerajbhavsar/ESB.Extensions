//---------------------------------------------------------------------
// File: FileMultiValidateStepEx.cs
// 
// Summary: 
//
//---------------------------------------------------------------------
// Copyright (c) 2004-2009, Kevin B. Smith. All rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------

namespace BizUnit
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Xml;
    using BizUnitOM;

    /// <summary>
    /// The FileMultiValidateStepEx step checks a given directory for files matching the file masks and iterates around all of the specified validate steps
    /// to validate the file.
    /// </summary>
    /// 
    /// <remarks>
    /// The following shows an example of the Xml representation of this test step.
    /// 
    /// <code escaped="true">
    /// <TestStep assemblyPath="" typeName="BizUnit.FileMultiValidateStepEx">
    ///     <Timeout>6000</Timeout>
    ///     <Directory>.\Rec_01</Directory>
    ///     <SearchPattern>*.xml</SearchPattern>
    ///     <FileCount>5</FileCount>
    ///     <DeleteFiles>true</DeleteFiles>
    ///			
    ///		<!-- Note: Validation step could be any generic validation step -->	
    ///		<ValidationStep assemblyPath="" typeName="BizUnit.XmlValidationStep">
    ///		    <XmlSchemaPath>.\TestData\PurchaseOrder.xsd</XmlSchemaPath>
    ///			<XmlSchemaNameSpace>http://SendMail.PurchaseOrder</XmlSchemaNameSpace>
    ///			<XPathList>
    ///			    <XPathValidation query="/*[local-name()='PurchaseOrder' and namespace-uri()='http://SendMail.PurchaseOrder']/*[local-name()='PONumber' and namespace-uri()='']">PONumber_0</XPathValidation>
    ///			</XPathList>
    ///     </ValidationStep>
    ///     			
    /// </TestStep>
    /// </code>
    ///	
    ///	<list type="table">
    ///		<listheader>
    ///			<term>Tag</term>
    ///			<description>Description</description>
    ///		</listheader>
    ///		<item>
    ///			<term>Timeout</term>
    ///			<description>Time to wait before checking</description>
    ///		</item>
    ///		<item>
    ///			<term>DirectoryPath</term>
    ///			<description>Directory path to check</description>
    ///		</item>
    ///		<item>
    ///			<term>SearchPattern</term>
    ///			<description>Matching pattern for files</description>
    ///		</item>
    ///		<item>
    ///			<term>ValidationStep</term>
    ///			<description>The validation step to use against the files <para>(one or more)</para></description>
    ///		</item>
    ///	</list>
    ///	</remarks>

    public class FileMultiValidateStepEx : ITestStep
    {
        private int timeout;
        private string directory;
        private string searchPattern;
        private bool deleteFiles;
        private int? filesCount;
        private IValidationStepOM validationStep;
        private IContextLoaderStepOM contextLoaderStep;
        private XmlNode validationConfig;
        private XmlNode contextConfig;

        public int Timeout
        {
            set { this.timeout = value; }
        }

        public string Directory
        {
            set { this.directory = value; }
        }

        public string SearchPattern
        {
            set { this.searchPattern = value; }
        }

        public bool DeleteFiles
        {
            set { this.deleteFiles = value; }
        }

        public int? FilesCount
        {
            set { this.filesCount = value; }
        }

        public IValidationStepOM ValidationStep
        {
            set { this.validationStep = value; }
        }

        public IContextLoaderStepOM ContextLoaderStep
        {
            set { this.contextLoaderStep = value; }
        }

        /// <summary>
        /// ITestStep.Execute() implementation
        /// </summary>
        /// <param name='testConfig'>The Xml fragment containing the configuration for this test step</param>
        /// <param name='context'>The context for the test, this holds state that is passed beteen tests</param>
        public void Execute(XmlNode testConfig, Context context)
        {
            this.directory = context.ReadConfigAsString(testConfig, "Directory");
            this.searchPattern = context.ReadConfigAsString(testConfig, "SearchPattern");
            this.deleteFiles = context.ReadConfigAsBool(testConfig, "DeleteFiles");
            this.timeout = context.ReadConfigAsInt32(testConfig, "Timeout");
            string fileCountString = context.ReadConfigAsString(testConfig, "FileCount", true);
            if (!string.IsNullOrEmpty(fileCountString))
            {
                this.filesCount = Convert.ToInt32(fileCountString);
            }

            this.validationConfig = testConfig.SelectSingleNode("ValidationStep");
            this.contextConfig = testConfig.SelectSingleNode("ContextLoaderStep");

            Execute(context);
        }

        /// <summary>
        /// ITestStep.Execute() implementation
        /// </summary>
        /// <param name='context'>The context for the test, this holds state that is passed beteen tests</param>
        public void Execute(Context context)
        {
            Thread.Sleep(this.timeout);
            string[] filelist = System.IO.Directory.GetFiles(this.directory, this.searchPattern);

            // Validate FileCount
            if (filelist.Length != this.filesCount)
            {
                throw new ApplicationException("Incorrect number of files.");
            }

            // For each file in the file list
            foreach (string filePath in filelist)
            {
                context.LogInfo("FileXmlValidateStep validating file: {0}", filePath);

                MemoryStream xmlData = StreamHelper.LoadFileToStream(filePath, timeout);
                StreamHelper.WriteStreamToConsole("File data to be validated", xmlData, context);

                // Try the validation and catch the exception
                xmlData.Seek(0, SeekOrigin.Begin);
                context.ExecuteValidator(xmlData, this.validationConfig);

                context.ExecuteContextLoader(xmlData, this.contextConfig);
            }

            if (this.deleteFiles)
            {
                foreach (string filePath in filelist)
                {
                    File.Delete(filePath);
                }
            }
        }

        /// <summary>
        /// ITestStepOM.Validate() implementation
        /// </summary>
        /// <param name='context'>The context for the test, this holds state that is passed beteen tests</param>
        public void Validate(Context context)
        {
            if (0 > this.timeout)
            {
                throw new ArgumentNullException("Timeout must be greater than zero");
            }

            if (null == this.directory || 0 == this.directory.Length)
            {
                throw new ArgumentNullException("Directory is either null or of zero length");
            }
            this.directory = context.SubstituteWildCards(this.directory);

            if (null == this.searchPattern || 0 == this.searchPattern.Length)
            {
                throw new ArgumentNullException("SearchPattern is either null or of zero length");
            }
            this.searchPattern = context.SubstituteWildCards(this.searchPattern);

            if ((this.filesCount.HasValue) && (this.filesCount < 0))
            {
                throw new ArgumentNullException("FilesCount smaller than zero");
            }

            if (null != this.validationStep)
            {
                this.validationStep.Validate(context);
            }

            if (null != this.contextLoaderStep)
            {
                this.contextLoaderStep.Validate(context);
            }
        }
    }
}
