/**
 * MobageUnityPeople
 */

using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using Proxy;


/*!
 * @abstract The social API for returning user objects with their social graphs. 
 * @discussion The user object retrieved varies from platform to platform. US platform required fields are <I>id</I>, 
 * <I>nickname</I>, <I>hasApp</I> and <I>thumbnailUrl</I> of the <I>Person</I> data type.
 */
public class MobagePeople
{

	/*!
	 * @abstract Retrieves the current user.
	 * @discussion Retrieves the <I>id</I>, <I>nickname</I>, <I>hasApp</I>
	 * and <I>thumbnailUrl</I> fields of the <I>Person</I> data type.
	 * To retrieve non-required fields of the <I>Person</I> data type, specify the non-required fields desired
	 * in the <I>fields</I> parameter.
	 * @param fields Takes an array of non-required fields (as string) to include in the response to the callback function. 
	 * <br/>If <I>null</I> or an empty array, it retrieves only the required fields.
	 * @param onSuccess Retrieves the current user.
	 * @param onError The callback interface that handles errors.
	 */
	public static void getCurrentUser(List<string> fields, 
	                                  GetCurrentUser.OnSuccessDelegate onSuccess, 
	                                  GetCurrentUser.OnErrorDelegate onError)
	{
		GetCurrentUser.Invock(fields, onSuccess, onError);
		return;
	}

	/*!
	 * @abstract Retrieves the friends of a given user.
	 * @discussion Retrieves the <I>id</I>, <I>nickname</I>, <I>hasApp</I> 
	 * and <I>thumbnailUrl</I> fields of the <I>Person</I> data type.
	 * To retrieve non-required fields of the <I>Person</I> data type, specify the non-required fields 
	 * desired in the <I>fields</I> parameter.
	 * Takes an optional <I>fields</I> parameter to retrieve non-required fields 
	 * of the <I>Person</I> data type, and an optional <I>opt</I> parameter for pagination.
	 * @param userId Takes a user ID of the user whose friends will be retrieved with the callback function. 
	 * @param fields Takes an array of non-required fields (as string) to include in the response to the callback function. 
	 * <br/> If <I>null</I> or an empty array, it retrieves only the required fields.
	 * @param options Takes a PagingOption structure containing the start index and count for pagination. <br/>E.g., <I>{start=1; count=100;}</I> 
	 * @param onSuccess Retrieves the friends of the given user .
	 * @param onError The callback interface that handles errors.
	 *
	 */
	public static void getFriends( string userId, List<string> fields, MobageOption option, 
	                                  PeopleUsersCallBackLib.OnSuccess onSuccess, 
	                                  PeopleUsersCallBackLib.OnError onError)
	{
		MobageManager.getFriends(userId, fields, option, onSuccess, onError);
		return;
	}

	/*!
	 * @abstract Retrieves the user's friends playing the current game.
	 * @discussion Takes an optional <I>fields</I> parameter to retrieve non-required fields of the 
	 * <I>Person</I> data type, and an optional <I>opt</I> parameter for pagination. 
	 * @param userId Takes a user ID of the user whose friends will be retrieved with the callback function. 
	 * @param fields Takes an array of non-required fields (as string) to include in the response to the callback function. 
	 * <br/> If <I>null</I> or an empty array, it retrieves only the required fields.
	 * @param options Takes a PagingOption structure containing the start index and count for pagination. <br/>E.g., <I>{start=1; count=100;}</I> 
	 * @param onSuccess Retrieves the friends of the given user.
	 * @param onError The callback interface that handles errors.
	 *
	 */
	public static void getFriendsWithGame( string userId, List<string> fields, MobageOption option, 
	                                  PeopleUsersCallBackLib.OnSuccess onSuccess, 
	                                  PeopleUsersCallBackLib.OnError onError)
	{
		MobageManager.getFriendsWithGame(userId, fields, option, onSuccess, onError);
		return;
	}

	/*!
	 * @abstract Retrieves the user corresponding to the user ID parameter.
	 * @discussion Takes a user ID parameter and retrieves the corresponding user. 
	 * Retrieves the <I>id</I>, <I>nickname</I>, <I>hasApp</I> 
	 * and <I>thumbnailUrl</I> fields of the <I>Person</I> data type.
	 * To retrieve non-required fields of the <I>Person</I> data type, specify the non-required fields 
	 * desired in the <I>fields</I> parameter.
	 * @param userId Takes the user ID of the user to retrieve.
	 * @param fields Takes an array of non-required fields (as string) to include in the response to the callback function. 
	 * <br/>If <I>null</I> or an empty array, it retrieves only the required fields.
	 * @param onSuccess Retrieves the user specified by the userId parameter.
	 * @param onError The callback interface that handles errors.
	 *
	 */
	public static void getUser( string userId, List<string> fields, 
	                                  GetUser.OnSuccessDelegate onSuccess, 
	                          		  GetUser.OnErrorDelegate onError)
	{
		GetUser.Invock(userId, fields, onSuccess, onError);
	}

	/*!
	 * @abstract Retrieves an array of users.
	 * @discussion Takes one or more user IDs and retrieves an array of users with the callback function. 
	 * Retrieves up to 100 users, with no guarantee of retrieving the user array 
	 * in the order specified in the <I>fields</I> parameter.
	 * Retrieves the <I>id</I>, <I>nickname</I>, <I>hasApp</I> 
	 * and <I>thumbnailUrl</I> fields of the <I>Person</I> data type for each user.
	 * To retrieve non-required fields of the <I>Person</I> data type, specify the non-required fields 
	 * desired in the <I>fields</I> parameter.
	 * @param userIds Takes an array of user IDs of the users to retrieve. Minimum array size is 1, and maximum is 25.
	 * @param fields Takes an array of non-required fields (as string) to include in the response to the callback function. 
	 * <br/>If <I>null</I> or an empty array, it retrieves only the required fields.
	 * @param onSuccess Retrieves an array of users specified by the <I>userIds</I> parameter.
	 * @param onError The callback interface that handles errors.
	 *
	 */
	public static void getUsers( List<string> userIds, List<string> fields, 
	                                  PeopleUsersCallBackLib.OnSuccess onSuccess, 
	                                  PeopleUsersCallBackLib.OnError onError)
	{
		MobageManager.getUsers(userIds, fields, onSuccess, onError);
		return;
	}

};
