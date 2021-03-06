CREATE DATABASE EsbBatchDb
GO

USE [EsbBatchDb]
GO
/****** Object:  Table [dbo].[BATCH]    Script Date: 11/18/2009 22:13:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BATCH](
	[PK] [bigint] IDENTITY(1,1) NOT NULL,
	[BatchId] [nvarchar](50) NOT NULL,
	[TimeStamp] [smalldatetime] NOT NULL,
	[DeterministicSequenceIds] [bit] NOT NULL,
	[IsComplete] [bit] NOT NULL,
	[IsPending] [bit] NOT NULL,
	[FirstSequenceId] [nvarchar](50) NULL,
	[NumberOfMessages] [bigint] NULL,
 CONSTRAINT [PK_BATCH] PRIMARY KEY CLUSTERED 
(
	[PK] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_BATCH_BatchId] ON [dbo].[BATCH] 
(
	[BatchId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-475124e4-0d3a-4746-8a13-cbe670252460]    Script Date: 11/18/2009 22:14:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-475124e4-0d3a-4746-8a13-cbe670252460] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-475124e4-0d3a-4746-8a13-cbe670252460]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-475124e4-0d3a-4746-8a13-cbe670252460] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN DROP SERVICE [SqlQueryNotificationService-475124e4-0d3a-4746-8a13-cbe670252460]; DROP QUEUE [SqlQueryNotificationService-475124e4-0d3a-4746-8a13-cbe670252460]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-475124e4-0d3a-4746-8a13-cbe670252460]; END COMMIT TRANSACTION; END
GO
/****** Object:  Table [dbo].[SEQUENCE]    Script Date: 11/18/2009 22:14:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SEQUENCE](
	[PK] [bigint] IDENTITY(1,1) NOT NULL,
	[BatchId] [nvarchar](50) NOT NULL,
	[SequenceId] [nvarchar](50) NOT NULL,
	[TimeStamp] [smalldatetime] NULL,
 CONSTRAINT [PK_SEQUENCE] PRIMARY KEY CLUSTERED 
(
	[PK] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_SEQUENCE_BatchId] ON [dbo].[SEQUENCE] 
(
	[BatchId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_SEQUENCE_BatchId_SequenceId] ON [dbo].[SEQUENCE] 
(
	[BatchId] ASC,
	[SequenceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_MergeBatchSequence]    Script Date: 11/18/2009 22:14:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_MergeBatchSequence] 
	-- Add the parameters for the stored procedure here
	@BatchId varchar(50),
	@SequenceId varchar(50),
	@DeterministicSequenceIds bit = 1,
	@IsFirstSequenceId bit = 0,
	@NumberOfMessages int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    IF @IsFirstSequenceId = 1
    BEGIN
		MERGE INTO dbo.BATCH as ba
		USING (SELECT @BatchId as BatchId, @DeterministicSequenceIds as DeterministicSequenceIds, @SequenceId as FirstSequenceId, @NumberOfMessages as NumberOfMessages) as NewBatch
		ON NewBatch.BatchId = ba.BatchId
		WHEN MATCHED THEN
			UPDATE SET ba.BatchId = NewBatch.BatchId, ba.DeterministicSequenceIds = NewBatch.DeterministicSequenceIds, ba.FirstSequenceId = NewBatch.FirstSequenceId, ba.NumberOfMessages = NewBatch.NumberOfMessages
		WHEN NOT MATCHED THEN
			INSERT (BatchId, DeterministicSequenceIds, FirstSequenceId, NumberOfMessages)
			VALUES (@BatchId, @DeterministicSequenceIds, @SequenceId, @NumberOfMessages);
	END
	ELSE
	BEGIN
		MERGE INTO dbo.BATCH as ba
		USING (SELECT @BatchId as BatchId, @DeterministicSequenceIds as DeterministicSequenceIds, NULL as FirstSequenceId, @NumberOfMessages as NumberOfMessages) as NewBatch
		ON NewBatch.BatchId = ba.BatchId
		WHEN MATCHED THEN
			UPDATE SET ba.BatchId = NewBatch.BatchId, ba.DeterministicSequenceIds = NewBatch.DeterministicSequenceIds, ba.FirstSequenceId = NewBatch.FirstSequenceId, ba.NumberOfMessages = NewBatch.NumberOfMessages
		WHEN NOT MATCHED THEN
			INSERT (BatchId, DeterministicSequenceIds, FirstSequenceId, NumberOfMessages)
			VALUES (@BatchId, @DeterministicSequenceIds, NULL, @NumberOfMessages);
	END
	
	MERGE INTO dbo.SEQUENCE as se
	USING (SELECT @BatchId as BatchId, @SequenceId as SequenceId) as NewSequence
	ON NewSequence.BatchId = se.BatchId AND NewSequence.SequenceId = se.SequenceId
	WHEN MATCHED THEN
		UPDATE SET se.BatchId = NewSequence.BatchId, se.SequenceId = NewSequence.SequenceId
	WHEN NOT MATCHED THEN
		INSERT (BatchId, SequenceId)
		VALUES (@BatchId, @SequenceId);

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllCompletedBatches]    Script Date: 11/18/2009 22:14:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllCompletedBatches]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION Tx1

    -- Insert statements for procedure here
	SELECT se.BatchId, se.SequenceId FROM dbo.BATCH ba, dbo.SEQUENCE se
	WHERE ba.IsComplete = 1 AND ba.IsPending = 1 AND se.SequenceId =
			(SELECT TOP 1 se.SequenceId
				FROM dbo.SEQUENCE se
				WHERE se.BatchId = ba.BatchId
				ORDER BY se.SequenceId)
				
	UPDATE dbo.BATCH
	SET IsPending = 0
	WHERE IsComplete = 1 AND IsPending = 1
				
	COMMIT TRANSACTION Tx1
END
GO
/****** Object:  Default [DF_BATCH_TimeStamp]    Script Date: 11/18/2009 22:13:58 ******/
ALTER TABLE [dbo].[BATCH] ADD  CONSTRAINT [DF_BATCH_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
/****** Object:  Default [DF_BATCH_DeterministicSequenceIds]    Script Date: 11/18/2009 22:13:58 ******/
ALTER TABLE [dbo].[BATCH] ADD  CONSTRAINT [DF_BATCH_DeterministicSequenceIds]  DEFAULT ((1)) FOR [DeterministicSequenceIds]
GO
/****** Object:  Default [DF_BATCH_IsComplete]    Script Date: 11/18/2009 22:13:58 ******/
ALTER TABLE [dbo].[BATCH] ADD  CONSTRAINT [DF_BATCH_IsComplete]  DEFAULT ((0)) FOR [IsComplete]
GO
/****** Object:  Default [DF_BATCH_IsPending]    Script Date: 11/18/2009 22:13:58 ******/
ALTER TABLE [dbo].[BATCH] ADD  CONSTRAINT [DF_BATCH_IsPending]  DEFAULT ((1)) FOR [IsPending]
GO
/****** Object:  Default [DF_SEQUENCE_TimeStamp]    Script Date: 11/18/2009 22:14:00 ******/
ALTER TABLE [dbo].[SEQUENCE] ADD  CONSTRAINT [DF_SEQUENCE_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
/****** Object:  ForeignKey [FK_BATCH_BATCH]    Script Date: 11/18/2009 22:13:58 ******/
ALTER TABLE [dbo].[BATCH]  WITH CHECK ADD  CONSTRAINT [FK_BATCH_BATCH] FOREIGN KEY([PK])
REFERENCES [dbo].[BATCH] ([PK])
GO
ALTER TABLE [dbo].[BATCH] CHECK CONSTRAINT [FK_BATCH_BATCH]
GO
/****** Object:  ForeignKey [FK_SEQUENCE_BATCH]    Script Date: 11/18/2009 22:14:00 ******/
ALTER TABLE [dbo].[SEQUENCE]  WITH CHECK ADD  CONSTRAINT [FK_SEQUENCE_BATCH] FOREIGN KEY([BatchId])
REFERENCES [dbo].[BATCH] ([BatchId])
GO
ALTER TABLE [dbo].[SEQUENCE] CHECK CONSTRAINT [FK_SEQUENCE_BATCH]
GO

USE [EsbBatchDb]
GO
/****** Object:  Trigger [dbo].[BatchComplete]    Script Date: 11/18/2009 22:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[BatchComplete]
   ON  [dbo].[SEQUENCE]
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
    UPDATE dbo.BATCH
    SET IsComplete=1
    WHERE dbo.BATCH.BatchId IN (SELECT DISTINCT(i.BatchId) FROM inserted i)
    AND	(SELECT COUNT(s.SequenceId) FROM dbo.SEQUENCE s WHERE s.BatchId = dbo.BATCH.BatchId) = dbo.BATCH.NumberOfMessages
    
END
