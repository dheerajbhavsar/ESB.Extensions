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
