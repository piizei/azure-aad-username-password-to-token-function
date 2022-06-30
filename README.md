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
