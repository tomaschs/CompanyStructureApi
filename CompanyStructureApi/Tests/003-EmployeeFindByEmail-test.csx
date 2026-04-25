using System.Text.Json;

await tp.Test("Find employee ID by email", async () =>
{
	var json = tp.Response.GetBody().ToString();

	var employees = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);

	string id = null;

	foreach (var emp in employees)
	{
		if (emp["email"].ToString() == "posttest@company.com")
		{
			id = emp["id"].ToString();
			break;
		}
	}

	True(id != null);

	tp.SetVariable("DeleteEmployeeId", id);
});