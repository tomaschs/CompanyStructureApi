await tp.Test("Delete employee", async () =>
{
	True(
		tp.Response.StatusCode() == 200 ||
		tp.Response.StatusCode() == 204 
	);
});