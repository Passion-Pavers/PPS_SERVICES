--SELECT * FROM public."Application";
	
-- Sample JSON data
DO $$
DECLARE
    json_data jsonb := '{
        "Title": "CredStore",
        "WelcomeMessage": "Welcome",
        "Footer": {
            "Title": "Footer Title",
            "Email": "DemoEmail@gmail.com",
            "Address": "Test Address"
        },
        "MainContent": {
            "GridTitle": "This is test grid"
        }
    }';
BEGIN
    UPDATE  public."Application"
    SET "AppConfigJson" = json_data
    WHERE "Id" = 1;  -- Add your condition here to update the specific record
END $$;


