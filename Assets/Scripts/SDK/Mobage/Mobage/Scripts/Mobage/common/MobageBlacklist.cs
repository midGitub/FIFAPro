/**
 * MobageUnityBlacklist
 */

using System;
using System.Collections;



/*!
 * @abstract Provides an interface for checking the user's blacklist. 
 */
public class MobageBlacklist {


	/*!
	 * @abstract Determines if a target user ID is in the given user's blacklist.
	 * @discussion Takes a user ID and a target user ID to determine if a target user is in the blacklist.
	 * If the target user ID parameter is <I>null</I>, it checks the entire blacklist. It retrives
	 * an array of target user IDs in the blacklist, or an empty array if none are found. The callback function also receives the total number of
	 * target user IDs, the starting index and the number of target IDs returned for paging results.
	 *
	 * @param userId The user ID for the user corresponding to the blacklist.
	 * @param targetUserId The target user ID to check in the blacklist. <br/>If <I>null</I>, it checks the entire blacklist.
	 * @param option Takes a PagingOption structure containing the start index and count for pagination. <br/>E.g., <I>{start=1, count=100}</I>
	 * Requires start index and count. The minimum start index is 1. If <I>null</I>, undefined or an empty array, the start index is 1 and the count is 50.
	 * @param onSuccess Retrieves an array of user IDs (as string) found in the blacklist; otherwise, it retrieves an empty array.
	 * @param onError The callback interface that handles errors.
	 */
	public static void checkBlacklist( string userId, string targetUserId, MobageOption options, 
	                                  BlacklistCallBackLib.OnSuccess onSuccess, 
	                                  BlacklistCallBackLib.OnError onError)
	{
		if(userId == null){
			MobageError err = new MobageError();
			err.description = "userId is empty";
			err.code = 400;
			onError(err);
			return;
		}
		
		MobageManager.checkBlacklist(userId, targetUserId, options, onSuccess, onError);
		return;
	}


}
