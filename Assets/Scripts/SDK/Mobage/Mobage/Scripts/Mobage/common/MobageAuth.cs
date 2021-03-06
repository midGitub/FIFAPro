/**
 * MobageUnityInitialization
 */

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;




/*!
 * @abstract Gets a verifier code.
 * @discussion Games call <I>Auth.authorizeToken()</I> if the game server is using the Mobage REST API.
 * For more information, refer Mobage REST API section of the developer's guide.
 */
public class MobageAuth {

	/*!
	 * @abstract Authorizes a temporary credential and returns a verification code.
	 * @param token The temporary credential identifier
	 * @param onSuccess The callback interface that handles the verification code
	 * @param onError The callback interface that handles errors.
	 *
	 */
	public static void authorizeToken(string token, 
	                                  AuthCallBackLib.OnSuccess onSuccess, 
	                                  AuthCallBackLib.OnError onError) {
		
		if(token == null){
			MobageError err = new MobageError();
			err.description = "token is empty";
			err.code = 400;
			onError(err);
			return;
		}
		
		MobageManager.authorizeToken(token, onSuccess, onError);
		return;
	}
	

}

