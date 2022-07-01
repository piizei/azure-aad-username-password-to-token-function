# azure-aad-username-password-to-token-function
Hack that logs in with username and password and returns a JWT token.

## Notes:
It does not (yet) have a JWT authentication but its planned. Currently you would need to protect it with function-key.

It excepts few env variables:
* TenantId: AAD tenant id of the AAD where the username-password is logged into.
* ClientId: App Registration client-id in the AAD where the username-password is logged into. This AppReg needs to be configured to allow this type of login.
* Scopes: Not mandatory as defaults to "User.Read".

The function excepts 2 query parameters
* username
* password
and returns a JWT token for that user.

You can use it together with APIM using this policy (Logs in with the function and forwards JWT token as Bearer)
Note: You would put the function url into Named Values of the gateway (as a secret, as it contains password...). 
Here the names value is called 'url'
```
<policies>
    <inbound>
        <send-request mode="new" response-variable-name="jwttoken" timeout="20" ignore-error="true">
            <set-url>{{url}}</set-url>
            <set-method>GET</set-method>
        </send-request>
        <set-header name="Authorization" exists-action="override">
            <value>@("Bearer "+((IResponse)context.Variables["jwttoken"]).Body.As<string>())</value>
        </set-header>
        <base />
    </inbound>
    <backend>
        <base />
    </backend>
    <outbound>
        <base />
    </outbound>
    <on-error>
        <base />
    </on-error>
</policies>
```

