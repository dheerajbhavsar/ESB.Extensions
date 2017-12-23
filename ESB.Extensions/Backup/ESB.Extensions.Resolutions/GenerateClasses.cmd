
xsd.exe ReceivePipelineServiceResolution.xsd /c /n:ESB.Extensions.Resolutions
xsd.exe SendPipelineServiceResolution.xsd /c /n:ESB.Extensions.Resolutions
rem xsd.exe ..\ESB.Extensions.Schemas\Go.xsd /c /n:ESB.Extensions.Resolutions /o:.\
xsd.exe ..\ESB.Extensions.Schemas\Go.xsd ..\ESB.Extensions.Schemas\GoList.xsd /c /n:ESB.Extensions.Resolutions /o:.\

xsd.exe ..\ESB.Extensions.Schemas.EsbBatchDb\MergeBatchSequence.xsd /c /n:ESB.Extensions.Resolutions /o:.\
