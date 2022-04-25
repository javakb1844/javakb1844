/****** Object:  DdlTrigger [Tr_AuditTables]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TRIGGER Tr_AuditTables
ON DATABASE
FOR
    CREATE_TABLE,
    ALTER_TABLE,
    DROP_TABLE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.AuditTable (
        Event_Data,
        ChangedBy,
		ChangedOn
    )
    VALUES (
        EVENTDATA(),
        USER,
		GETDATE()
    );
END
ENABLE TRIGGER [Tr_AuditTables] ON DATABASE
