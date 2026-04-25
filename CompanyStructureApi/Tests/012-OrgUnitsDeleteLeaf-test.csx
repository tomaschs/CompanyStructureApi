await tp.Test("Delete leaf OrgUnit returns 204", async () =>
{
	Equal(204, tp.Response.StatusCode());
});