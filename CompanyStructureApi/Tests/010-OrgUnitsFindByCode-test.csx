using System.Text.Json;

await tp.Test("Find OrgUnit ID by code", async () =>
{
	var json = tp.Response.GetBody().ToString();

	var orgUnits = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);

	string id = null;

	foreach (var item in orgUnits)
	{
		if (item["code"].ToString() == "TP_DIV_001")
		{
			id = item["id"].ToString();
			break;
		}
	}

	True(id != null);

	tp.SetVariable("DeleteOrgUnitId", id);
});