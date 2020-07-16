# EXILEDPluginUpdater
 Easy auto updater for EXILED plugins

## Devs

- Implement `IAutoUpdater`

- Set the url to the json file

- Update Json file when releasing new updates

Example Json
```json
{
    "Versions": [
        {
            "DllUrl": "https://example.com",
            "VersionId": {
                "Major": 1,
                "Minor": 1,
                "Build": 2,
                "Revision": 0,
                "MajorRevision": 0,
                "MinorRevision": 0
            }
        }
    ]
}
```

Example Plugin: https://github.com/VirtualBrightPlayz/EXILEDCustomSpawns
