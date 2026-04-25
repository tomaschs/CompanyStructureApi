await tp.Test("POST OrgUnit invalid root type returns 400", async () =>
{
	Equal(400, tp.Response.StatusCode());
});