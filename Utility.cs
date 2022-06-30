using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Threading.Tasks;
using System.Web;

public static class Utility
{

    static IPublicClientApplication publicClientApp = PublicClientApplicationBuilder
               .Create(Environment.GetEnvironmentVariable("ClientId"))
               .WithTenantId(Environment.GetEnvironmentVariable("TenantId")).Build();
	public static void AddAuthorizationHeader(this HttpClient client, String accessToken)
	{
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
	}

    public static async Task<string> GetAccessTokenAsync(String[] scopes, string username, string password)
    {
        var spassword = new SecureString();
        foreach (char c in password) spassword.AppendChar(c);
        AuthenticationResult authenticationResult = await GetToken(scopes, username, spassword);
        if (authenticationResult != null)
        {            
            return authenticationResult.AccessToken;            
        }
        return "";
    }

    private static async Task<AuthenticationResult> GetToken(IEnumerable<string> scopes, string username, SecureString password)
    {
        AuthenticationResult result = null;
        
        try
        {
            result = await publicClientApp.AcquireTokenByUsernamePassword(scopes, username, password)
                .ExecuteAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        return result;
    }

    public static String UrlDecode(this String str)
    {
        return HttpUtility.UrlDecode(str);
    }

}
