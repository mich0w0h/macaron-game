#if UNITY_EDITOR
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;


public class ScenarioLoader {
    private  async Task<IList<IList<object>>> LoadSheetData(string spreadsheetId, string sheetName) {
        string credentialPath = "SheetJson/sheet-scenario-loader.json";
        string[] scopes = { SheetsService.Scope.SpreadsheetsReadonly };

        GoogleCredential credential = GoogleCredential.FromFile(credentialPath).CreateScoped(scopes);

        SheetsService service = new (new BaseClientService.Initializer() {
            HttpClientInitializer = credential
        });

        SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, sheetName);

        ValueRange response = await request.ExecuteAsync();

        return response.Values;
    }

    private List<DayScenario> CreateDayScenarioList(IList<IList<object>> values) {
        values.RemoveAt(0); // skip header
        const int typeColumn = 2;
        return values
            .Where(column => column.Count > typeColumn)
            .Select(column => {                
                return new DayScenario() {
                    Key = $"{column[0]}_{column[1]}",
                    Contents = column.Skip(typeColumn).Select(line => line.ToString()).ToArray()
                };
            })
            .ToList();
    }

    public async Task<ScenarioData> CreateScenarioData() {
        const string spreadsheetId = "1AO5JlJLKhhHZ2xxU1Lvs9-jQ1xViRkpUjhw5dny34rY";
        const string sheetName = "Scenario";

        IList<IList<object>> values = await LoadSheetData(spreadsheetId, sheetName);
        
        if (values == null || values.Count == 0) {
            Debug.Log($"The specified sheet '{sheetName}' has no data.");
            return null;
        }        

        ScenarioData scenarioData = ScriptableObject.CreateInstance<ScenarioData>();
        scenarioData.Init(CreateDayScenarioList(values));
        
        return scenarioData;
    }
}

#endif
