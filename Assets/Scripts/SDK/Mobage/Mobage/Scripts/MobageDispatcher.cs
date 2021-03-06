
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Threading;
using MobageLitJson;

public class MobageDispatcher{



	static public string ToShiftJis(string unicodeStrings)
	{
		Encoding unicode = Encoding.Unicode;
		byte[] unicodeByte = unicode.GetBytes(unicodeStrings);
		Encoding s_jis = Encoding.GetEncoding("shift_jis");
		byte[] s_jisByte = Encoding.Convert(unicode,s_jis,unicodeByte);
		char[] s_jisChars = new char[s_jis.GetCharCount(s_jisByte,0,s_jisByte.Length)];
		s_jis.GetChars(s_jisByte,0,s_jisByte.Length,s_jisChars,0);
		return new string(s_jisChars);
	}
	
	static public string ToUTF8(string unicodeStrings)
	{
		Encoding unicode = Encoding.Unicode;
		byte[] unicodeByte = unicode.GetBytes(unicodeStrings);
		Encoding s_utf8 = Encoding.GetEncoding("utf-8");
		byte[] s_utf8Byte = Encoding.Convert(unicode,s_utf8,unicodeByte);
		char[] s_utf8Chars = new char[s_utf8.GetCharCount(s_utf8Byte,0,s_utf8Byte.Length)];
		s_utf8.GetChars(s_utf8Byte,0,s_utf8Byte.Length,s_utf8Chars,0);
		return new string(s_utf8Chars);
	}

	static public string UTF8ToUnicode(string utf8Strings)
	{
		Encoding utf8 = Encoding.UTF8;
		byte[] utf8Byte = utf8.GetBytes(utf8Strings);
		Encoding s_unicode = Encoding.GetEncoding("unicode");
		byte[] s_unicodeByte = Encoding.Convert(utf8,s_unicode,utf8Byte);
		char[] s_unicodeChars = new char[s_unicode.GetCharCount(s_unicodeByte,0,s_unicodeByte.Length)];
		s_unicode.GetChars(s_unicodeByte,0,s_unicodeByte.Length,s_unicodeChars,0);
		return new string(s_unicodeChars);
	}
	


	public static string getfriends(string userId, List<string> fields, MobageOption option) 
	{
		int i = 0;
		string chunk = "";
		
		chunk = "{";
		chunk += "userId::";
		chunk += userId;
		chunk += ",";
		chunk += "user::{";
		while ( i < fields.Count ) {
			chunk += fields[i] as string;
			i++;
			if(i < fields.Count){
				chunk += ",";
			}else{
				break;
			}
 		}
		chunk += "}";
		chunk += ",";
		chunk += "OptStart::";
		chunk += option.start.ToString();
		chunk += ",";
		chunk += "OptCount::";
		chunk += option.count.ToString();
		chunk += "}";
		
		return ToUTF8(chunk);
	}
	
	public static string getfriendswithGame(string userId, List<string> fields, MobageOption option) 
	{
		int i = 0;
		string chunk = "";
		
		chunk = "{";
		chunk += "userId::";
		chunk += userId;
		chunk += ",";
		chunk += "user::{";
		while ( i < fields.Count ) {
			chunk += fields[i] as string;
			i++;
			if(i < fields.Count){
				chunk += ",";
			}else{
				break;
			}
 		}
		chunk += "}";
		chunk += ",";
		chunk += "OptStart::";
		chunk += option.start.ToString();
		chunk += ",";
		chunk += "OptCount::";
		chunk += option.count.ToString();
		chunk += "}";

		return ToUTF8(chunk);
	}
	

	
	public static string getusers(List<string> userIds, List<string> fields) 
	{
		int i = 0;
		string chunk;

		chunk = "{";
		chunk += "userId::{";
		while ( i < userIds.Count ) {
			chunk += userIds[i] as string;
			i++;
			if(i < userIds.Count){
				chunk += ",";
			}else{
				break;
			}
 		}
		chunk += "}";
		chunk += ",";
		chunk += "user::{";
		while ( i < fields.Count ) {
			chunk += fields[i] as string;
			i++;
			if(i < fields.Count){
				chunk += ",";
			}else{
				break;
			}
 		}
		chunk += "}";
		chunk += "}";

		return ToUTF8(chunk);
	}
	
	public static string checkblacklist(string userId, string targetUserId, MobageOption options) 
	{
		string chunk;
		chunk = "{";
		chunk += "userId::";
		chunk += userId;
		chunk += ",";
		chunk += "targetUserId::";
		if(targetUserId==null){
			chunk += "null";
		}else{
			chunk += targetUserId;
		}
		chunk += ",";
		chunk += "OptStart::";
		chunk += options.start.ToString();
		chunk += ",";
		chunk += "OptCount::";
		chunk += options.count.ToString();
		chunk += "}";

		return ToUTF8(chunk);
	}
	
	public static string createtransaction(List<MobageBillingItem> items, string comment)
	{
		int i = 0;
		string chunk;

		chunk = "{";
		while ( i < items.Count ) {
			chunk += "{id::";
			chunk += items[i].item.id;
			chunk += ",";
			chunk += "name::";
			chunk += items[i].item.name;
			chunk += ",";
			chunk += "price::";
			chunk += items[i].item.price;
			chunk += ",";
			chunk += "description::";
			chunk += items[i].item.description;
			chunk += ",";
			chunk += "imageUrl::";
			chunk += items[i].item.imageUrl;
			chunk += "}{quantity::";
			chunk += items[i].quantity;
			chunk += "}";
			i++;
			if(i == items.Count){
				break;
			}else{
				chunk += ":";
			}
		}
		chunk += "}";
		chunk += ",";
		chunk += "Comment::";
		chunk += comment;
		chunk += "}";

		return ToUTF8(chunk);
	}

	// Remote Notification
	public static string send( string recipientId, MobageRemoteNotificationPayload data) 
	{
		string chunk = "";
/*
		string chunk = JsonMapper.ToJson (recipientId);
		string chunk = JsonMapper.ToJson (data);
*/		
		chunk = "{recipientId::";
		chunk += recipientId;
		chunk += "},{";
		chunk += "badge::";
		chunk += data.badge;
		chunk += ",";
		chunk += "message::";
		chunk += data.message;
		chunk += ",";
		chunk += "sound::";
		chunk += data.sound;
		chunk += ",";
		chunk += "collapseKey::";
		chunk += data.collapseKey;
		chunk += ",";
		chunk += "style::";
		chunk += data.style;
		chunk += ",";
		chunk += "iconUrl::";
		chunk += data.iconUrl;
		chunk += ",";
		chunk += "extras::";
		int i = 0;
		if(data.extras != null && data.extras.Count > 0){
//			chunk = "{";
			foreach (string key in data.extras.Keys) {
		    	chunk += key;
				chunk += ":";
				chunk += data.extras[key];
					i++;
					if(i < data.extras.Count){
						chunk += ",";
					}
	    	}
//			chunk += "}";
		}
		chunk += "}";

		return ToUTF8(chunk);
	}

	public static string setRemoteNotificationsEnabled( bool enabled ) 
	{
		string chunk = enabled? "true":"false";

		return chunk;
	}
}

public class LogoutListener{
	
	
	public delegate void OnLogoutComplete();
	
	public OnLogoutComplete CallBack_onLogoutComplete { get; set;}
	
	public void onNativeLogoutComplete()
	{
		if (CallBack_onLogoutComplete != null) CallBack_onLogoutComplete ();
	}
}


//!;
public static class SwitchAccountProxy{
	
	
	public delegate void OnSwitchAccount(bool success);
	
	public static OnSwitchAccount CallBack_onSwitchAccount { get; set;}
	
	public static void onNativeSwitchAccount(string key)
	{
		try
		{
			JsonData jo = JsonMapper.ToObject (key);
			bool success = (bool)jo["success"];
			if (CallBack_onSwitchAccount != null) CallBack_onSwitchAccount (success);
		}
		catch
		{
			throw new Exception("Json String invalid");
		}
	}
}



//XPromotionListener
public static class XPromotionListener{
	
	
	public delegate void DidShow();
	public delegate void DidClose();
	public delegate void DidClick();
	
	public static DidShow CallBack_DidShow { get; set;}
	public static DidClose CallBack_DidClose { get; set;}
	public static DidClick CallBack_DidClick { get; set;}
	
	public static void onNative_DidShow()
	{
		/*if (CallBack_DidShow != null)*/ CallBack_DidShow ();
	}

	public static void onNative_DidClose()
	{
		if (CallBack_DidClose != null) CallBack_DidClose ();
	}

	public static void onNative_DidClick()
	{
		if (CallBack_DidClick != null) CallBack_DidClick ();
	}
}

namespace BalanceButtonCallBackLib
{
	public delegate void OnSuccess(Balance item);

	public delegate void OnError(MobageError err);
	
	public class OnComplete{
		public OnComplete()
		{
		}
		
		private OnSuccess onSuccess;
		private OnError onError;
		
		public void SetSuccessCallBack(OnSuccess del)
		{
			onSuccess = del;
		}
		
		public void SetErrorCallBack(OnError del)
		{
			onError = del;
		}
		
		public void GetonSuccess(string key)
		{
			string[] ss = key.Split(',');
			string balance = ss.Length > 0 ? ss[0] : "0";
			string limitation = ss.Length > 0 ? ss[1] : "0";
			string state = ss.Length > 0 ? ss[2] : "";

			Balance b = new Balance(balance, limitation, state);
			onSuccess(b);

		}
		
		public void GetonError(string key)
		{
			MobageError out_err;
			try
			{
				out_err = JsonMapper.ToObject<MobageError> (key);
				onError(out_err);
			}
			catch
			{
				out_err = new MobageError();
				out_err.code = 500;
				out_err.description = "Internal error.";
				onError(out_err);
			}
		}
	}
}


/*!
 * @abstract Authorizes a temporary credential and returns a verification code.
 */
namespace AuthCallBackLib
{
	/*!
	 * @abstract The callback interface that handles the verification code
	 */
	public delegate void OnSuccess(string verifier);

	/*!
	 * @abstract The callback interface that handles errors.
	 */
	public delegate void OnError(MobageError err);
	
	public class OnComplete{
		public OnComplete()
		{
		}
		
		private OnSuccess onSuccess;
		private OnError onError;

		public void SetSuccessCallBack(OnSuccess del)
        {
            onSuccess = del;
        }

		public void SetErrorCallBack(OnError del)
        {
            onError = del;
        }

		public void GetonSuccess(string utf8string)
		{
			string out_str = MobageDispatcher.UTF8ToUnicode(utf8string);
			
			onSuccess(out_str);
		}
		
		public void GetonError(string key)
		{
			MobageError out_err = new MobageError();
			out_err.description = key;
			onError(out_err);

			/*
			MobageError out_err;
			try
			{
				out_err = JsonMapper.ToObject<MobageError> (key);			
				onError(out_err);
			}
			catch
			{
				out_err = new MobageError();
				out_err.code = 500;
				out_err.description = "Internal error.";
				onError(out_err);
			}
			*/

		}
	}
}


/*!
 * @abstract Determines if a target user ID is in the given user's blacklist.
 * @discussion Takes a user ID and a target user ID to determine if a target user is in the blacklist.
 * If the target user ID parameter is null, it checks the entire blacklist. It retrives
 * an array of target user IDs in the blacklist, or an empty array if none are found. The callback function also receives the total number of
 * target user IDs, the starting index and the number of target IDs returned for paging results.
 */
namespace BlacklistCallBackLib
{
	/*!
	 * @abstract Retrieves an array of user IDs (as NSString) found in the blacklist; otherwise, it retrieves an empty array.
	 */
	public delegate void OnSuccess(List<string> listedUsers, MobageResult result);

	/*!
	 * @abstract The callback interface that handles errors.
	 */
	public delegate void OnError(MobageError err);
	
	public class OnComplete {
		public OnComplete()
		{
		}
		
		private OnSuccess onSuccess;
		private OnError onError;

		public void SetSuccessCallBack(OnSuccess del)
        {
            onSuccess = del;
        }

		public void SetErrorCallBack(OnError del)
        {
            onError = del;
        }

		public void GetonSuccess(string utf8string)
		{
			string key = MobageDispatcher.UTF8ToUnicode(utf8string);

			int mResult = key.IndexOf("{start::");
			List<string> out_val = new List<string>();
			string users;
			if(mResult > 3){
				users = key.Substring(2, (mResult-4));
				string[] tmpbuf = users.Split(',');
				foreach (string s in tmpbuf) {
					out_val.Add(s);
				}
			}

			MobageResult result = new MobageResult();
			mResult+=8;
			for(int cnt = mResult ; cnt<key.Length ; cnt++){
				if(key[cnt]=='}'){
					if(cnt==mResult){
						result.setStart(0);
						break;
					}else{
						result.setStart(int.Parse(key.Substring(mResult,(cnt-mResult))));
						break;
					}
				}
			}
			mResult = key.IndexOf("{count::");
			mResult+=8;
			for(int cnt = mResult ; cnt<key.Length ; cnt++){
				if(key[cnt]=='}'){
					if(cnt==mResult){
						result.setCount(0);
						break;
					}else{
						result.setCount(int.Parse(key.Substring(mResult,(cnt-mResult))));
						break;
					}
				}
			}
			mResult = key.IndexOf("{total::");
			mResult+=8;
			for(int cnt = mResult ; cnt<key.Length ; cnt++){
				if(key[cnt]=='}'){
					if(cnt==mResult){
						result.setTotal(0);
						break;
					}else{
						result.setTotal(int.Parse(key.Substring(mResult,(cnt-mResult))));
						break;
					}
				}
			}
			
			onSuccess(out_val, result);
		}
		
		public void GetonError(string key)
		{
			MobageError out_err;
			try
			{
				out_err = JsonMapper.ToObject<MobageError> (key);
				onError(out_err);
			}
			catch
			{
				out_err = new MobageError();
				out_err.code = 500;
				out_err.description = "Internal error.";
				onError(out_err);
			}
		}
	}
}


/*!
 * @abstract Retrieves the user.
 */
namespace PeopleUsersCallBackLib
{
	/*!
	 * @abstract Retrieves the friends of the given user 
	 */
	public delegate void OnSuccess(List<MobageUser> users, MobageResult result);

	/*!
	 * @abstract The callback interface that handles errors.
	 */
	public delegate void OnError(MobageError err);
	
	public class OnComplete {
		public OnComplete()
		{
		}
		
		private OnSuccess onSuccess;
		private OnError onError;

		public void SetSuccessCallBack(OnSuccess del)
        {
            onSuccess = del;
        }

		public void SetErrorCallBack(OnError del)
        {
            onError = del;
        }

		public void GetonSuccess(string utf8string)
		{
			string key = MobageDispatcher.UTF8ToUnicode(utf8string);
			
			int mUser = key.IndexOf("{Users::");
			int mResult = key.IndexOf("{start::");
			string users = key.Substring(mUser+8, ((mResult-2)-(mUser+8)));
			List<MobageUser> dataList = new List<MobageUser>();

			int head, tail;
			string sep = "},{";
			MobageUser out_user;
			string tmpbuf;
			if(mResult >= 12){
				for (head = 0 ; (tail = users.IndexOf(sep, head)) != -1;head = tail + sep.Length) {
					if(head==0){
						tmpbuf = userUtilityForUserAge(users.Substring(head, (tail+1) - head));
						out_user = JsonMapper.ToObject<MobageUser> (tmpbuf);
					}else{
						tmpbuf = userUtilityForUserAge(users.Substring((head-1), (tail+2) - head));
						out_user = JsonMapper.ToObject<MobageUser> (tmpbuf);
					}
					dataList.Add(out_user);
				}
				if(head==0){
					tmpbuf = userUtilityForUserAge(users.Substring(head, users.Length));
				}else{
					tmpbuf = userUtilityForUserAge(users.Substring(head-1));
				}
				out_user = JsonMapper.ToObject<MobageUser> (tmpbuf);
				dataList.Add(out_user);
			}
			
//			for(int i = 0;i<works.Length;i++) {
//				if(i==0){
//					works[i].Insert(works[i].Length, "}");
//				}else if((i+1)==works.Length){
//					works[i].Insert(0, "{");
//				}else{
//					works[i].Insert(0, "{");
//					works[i].Insert(works[i].Length, "}");
//				}

//				Debug.Log("CallBackLib; GetonSuccess ; "+":::"+i+":::"+works[i]);
//				MobageUser out_user = JsonMapper.ToObject<MobageUser> (works[i]);
//				dataList.Add(out_user);
//		    }			

			MobageResult result = new MobageResult();
			mResult+=8;
			for(int cnt = mResult ; cnt<key.Length ; cnt++){
				if(key[cnt]=='}'){
					if(cnt==mResult){
						result.setStart(0);
						break;
					}else{
						result.setStart(int.Parse(key.Substring(mResult,(cnt-mResult))));
						break;
					}
				}
			}
			mResult = key.IndexOf("{count::");
			mResult+=8;
			for(int cnt = mResult ; cnt<key.Length ; cnt++){
				if(key[cnt]=='}'){
					if(cnt==mResult){
						result.setCount(0);
						break;
					}else{
						result.setCount(int.Parse(key.Substring(mResult,(cnt-mResult))));
						break;
					}
				}
			}
			mResult = key.IndexOf("{total::");
			mResult+=8;
			for(int cnt = mResult ; cnt<key.Length ; cnt++){
				if(key[cnt]=='}'){
					if(cnt==mResult){
						result.setTotal(0);
						break;
					}else{
						result.setTotal(int.Parse(key.Substring(mResult,(cnt-mResult))));
						break;
					}
				}
			}

			onSuccess(dataList, result);
		}
		
		public void GetonError(string key)
		{
			MobageError out_err;
			try
			{
				out_err = JsonMapper.ToObject<MobageError> (key);
				onError(out_err);
			}
			catch
			{
				out_err = new MobageError();
				out_err.code = 500;
				out_err.description = "Internal error.";
				onError(out_err);
			}
		}

		// START:: to aboid LitJson exception cause NativeSDK interface is different(iOS, Android).
		private string userUtilityForUserAge(string key){
			int num = key.IndexOf("age");
			if(num<0){
				return key;
			}
			if(key[(num+4)]==':'){
				num+=5;
				if(key[num]=='"'){
					key = key.Remove(num,1);
				}
				for (/*num--*/ ; num < key.Length ; num++ ){
					if(key[num]==',' || key[num]=='}'){
						if(key[(num-1)]=='"'){
							key = key.Remove((num-1),1);
						}
						break;
					}
				}
			}
			return key;
		}
		// END::to aboid LitJson exception cause NativeSDK interface is different(iOS, Android).
	}
}

/*!
 * @abstract Delegate interface for Login, notifying completion of showing Mobage's splash screen 
 */
namespace AddLoginCallBackLib
{
	/*!
	 * @abstract The callback interface that complete an user login.
	 */
	public delegate void onLoginComplete(string userid);

	/*!
	 * @abstract The callback interface that platform require an user login.
	 */
	public delegate void onLoginRequired( );

	/*!
	 * @abstract The callback interface that handles Cancel event.
	 */
	public delegate void onCancel( );
	
	/*!
	 * @abstract The callback interface that handles errors.
	 */
	public delegate void onError(MobageError err);

	public class OnComplete{
		public OnComplete()
		{
		}
		
		private onLoginComplete onComp;
		private onLoginRequired oReqed;
		private onCancel onCancel;
		private onError onErr;

		public void SetCompCallBack(onLoginComplete del)
        {
            onComp = del;
        }

		public void SetReqedCallBack(onLoginRequired del)
        {
            oReqed = del;
        }

		public void SetCancelCallBack(onCancel del)
        {
            onCancel = del;
        }

		public void SetErrorCallBack(onError del)
        {
            onErr = del;
        }

		public void GetonComp(string key)
		{
			if(onComp != null) onComp(key);
		}
		
		public void GetonReqed(string key)
		{
			if(oReqed != null) oReqed( );
		}

		public void GetonCancel(string key)
		{
			if(onCancel != null) onCancel( );
		}

		public void GetonError(string key)
		{
			MobageError out_err;
//			try
//			{
				out_err = JsonMapper.ToObject<MobageError> (key);
				if(onErr != null) onErr(out_err);
//			}
//			catch
//			{
//				out_err = new MobageError();
//				out_err.code = 500;
//				out_err.description = "Internal error.";
//				onErr(out_err);
//			}
		}
	}
}


/*!
 * @abstract shows Bank dialog to purchase game currency
 */
namespace BalanceDialogCompleteCallBackLib
{
	/*!
	 * @abstract The callback interface to notify dismissing Webview dialog
	 */
	public delegate void onDismiss();
	
	public class OnComplete{
		public OnComplete()
		{
		}
		
		private onDismiss onDis;

		public void SetSuccessCallBack(onDismiss del)
        {
            onDis = del;
        }
		public void GetonSuccess( )
		{
			onDis();
		}
		
	}
}


/*!
 * @abstract shows information Page of Mobage 
 */
namespace DialogCompleteCallBackLib
{
	/*!
	 * @abstract The callback interface to notify dismissing alert dialog
	 */
	public delegate void onDismiss();
	
	public class OnComplete{
		public OnComplete()
		{
		}
		
		private onDismiss onDis;

		public void SetSuccessCallBack(onDismiss del)
        {
            onDis = del;
        }

		public void GetonSuccess( )
		{
			if(onDis != null) onDis();
		}
		
	}
}



/*!
 * @abstract shows bank transaction of Mobage 
 */
namespace BankTranDlgCmpCallBackLib
{
	/*!
	 * @abstract The callback interface to notify dismissing alert dialog
	 */
	public delegate void OnSuccess(MobageTransaction transaction);

	/*!
	 * @abstract The callback interface that handles errors.
	 */
	public delegate void OnError(MobageError err);

	/*!
	 * @abstract The callback interface that handles cancel event.
	 */
	public delegate void OnCancel( );
	
	public class OnComplete{
		public OnComplete()
		{
		}
		
		private OnSuccess onSuccess;
		private OnError onError;
		private OnCancel onCancel;

		public void SetSuccessCallBack(OnSuccess del)
        {
            onSuccess = del;
        }

		public void SetErrorCallBack(OnError del)
        {
            onError = del;
        }

		public void SetCancelCallBack(OnCancel del)
        {
            onCancel = del;
        }

		public void GetonSuccess(string key)
		{
			MobageTransaction out_data;
			if(key == "json serialize error:"){
				out_data = new MobageTransaction();
				out_data.comment = "json serialize error:";
			}else{
				out_data = JsonMapper.ToObject<MobageTransaction> (key);
			}

			onSuccess(out_data);
		}
		
		public void GetonError(string key)
		{
			MobageError out_err;
			try
			{
				out_err = JsonMapper.ToObject<MobageError> (key);
				onError(out_err);
			}
			catch
			{
				out_err = new MobageError();
				out_err.code = 500;
				out_err.description = "Internal error.";
				onError(out_err);
			}
		}

		public void GetonCancel(string key)
		{
			onCancel();
		}
	}
}

/*!
 * @abstract Creates a transaction with the transaction.state set to new.
 * @discussion Mobage presents a user interface that prompts the user to confirm the transaction.
 * If the user decides not to proceed with the transaction, the callback error will indicate "user canceled."
 * In the client-only flow, it checks inventory and sets the state to authorized.
 */
namespace BankTranCmpCallBackLib
{
	/*!
	 * @abstract callback Retrieves the transaction details.
	 */
	public delegate void OnSuccess(MobageTransaction transaction);

	/*!
	 * @abstract The callback interface that handles errors
	 */
	public delegate void OnError(MobageError err);
	
	public class OnComplete{
		public OnComplete()
		{
		}
		
		private OnSuccess onSuccess;
		private OnError onError;

		public void SetSuccessCallBack(OnSuccess del)
        {
            onSuccess = del;
        }

		public void SetErrorCallBack(OnError del)
        {
            onError = del;
        }

		public void GetonSuccess(string key)
		{
			MobageTransaction out_data;
			if(key == "json serialize error:"){
				out_data = new MobageTransaction();
				out_data.comment = "json serialize error:";
			}else{
				out_data = JsonMapper.ToObject<MobageTransaction> (key);
			}
			
			onSuccess(out_data);
		}
		
		public void GetonError(string key)
		{
			MobageError out_err;
			try
			{
				out_err = JsonMapper.ToObject<MobageError> (key);
				onError(out_err);
			}
			catch
			{
				out_err = new MobageError();
				out_err.code = 500;
				out_err.description = "Internal error.";
				onError(out_err);
			}
		}
	}
}


/*!
 * @abstract Provides an interface to retrieve items from inventory. 
 * @discussion The Mobage platform server is responsible for managing a game's item inventory.
 */
namespace BankInventryCallBackLib
{
	/*!
	 * @abstract Retrieves the item from inventory on the Mobage platform server.
	 */
	public delegate void OnSuccess(MobageItemData item);

	/*!
	 * @abstract Callback interface that handles errors.
	 */
	public delegate void OnError(MobageError err);
	
	public class OnComplete{
		public OnComplete()
		{
		}
		
		private OnSuccess onSuccess;
		private OnError onError;

		public void SetSuccessCallBack(OnSuccess del)
        {
            onSuccess = del;
        }

		public void SetErrorCallBack(OnError del)
        {
            onError = del;
        }

		public void GetonSuccess(string key)
		{
			MobageItemData out_item = JsonMapper.ToObject<MobageItemData> (key);

//			MobageItemData out_item = new MobageItemData();
//			{
//				string[] tmpMAP = key.Split(',');
//				foreach (string s in tmpMAP) {
//					string[] tmpEle = s.Split(':');
//					if(tmpEle[0] == "id"){
//						out_item.id = tmpEle[1];
//					}else if(tmpEle[0] == "mName"){
//						out_item.name = tmpEle[1];
//					}else if(tmpEle[0] == "mPrice"){
//						out_item.price = int.Parse(tmpEle[1]);
//					}else if(tmpEle[0] == "mDescription"){
//						out_item.description = tmpEle[1];
//					}else if(tmpEle[0] == "mImageUrl"){
//						out_item.imageUrl = tmpEle[1];
//					}
//				}
//			}
			onSuccess(out_item);
		}
		
		public void GetonError(string key)
		{
			MobageError out_err;
			try
			{
				out_err = JsonMapper.ToObject<MobageError> (key);
				onError(out_err);
			}
			catch
			{
				out_err = new MobageError();
				out_err.code = 500;
				out_err.description = "Internal error.";
				onError(out_err);
			}
		}
	}
}



/*!
 * @abstract returns market code(like mbga, amkt...)
 * Attention! this API is Android only implementation.
 */
namespace MarketCodeCallBackLib
{
	/*!
	 * @abstract returns market code(like mbga, amkt...)
	 */
	public delegate void OnGetMarketCode(int market_code);
	
	public class OnComplete{
		public OnComplete()
		{
		}
		
		private OnGetMarketCode onGetMarketCode;

		public void SetMarketCodeCallBack(OnGetMarketCode del)
        {
            onGetMarketCode = del;
        }

		public void GetMarketCode(int market_code)
		{
			onGetMarketCode(market_code);
		}
	}
}


/*!
 * @abstract Callback listener interface for send().
 */
namespace RemoteNotificationSendCallBackLib
{
	/*!
	 * @abstract onSuccess Retrieves the remote notification send response.
	 */
	//public delegate void OnSuccess(MobageRemoteNotificationResponse response);
	public delegate void OnSuccess(string response);

	/*!
	 * @abstract onError Callback interface that handles errors.
	 */
	public delegate void OnError(MobageError err);
	
	public class OnComplete {
		public OnComplete()
		{
		}
		
		private OnSuccess onSuccess;
		private OnError onError;

		public void SetSuccessCallBack(OnSuccess del)
        {
            onSuccess = del;
        }

		public void SetErrorCallBack(OnError del)
        {
            onError = del;
        }

		public void GetonSuccess(string utf8string)
		{
			onSuccess(utf8string);
			/*
			MobageRemoteNotificationResponse response = new MobageRemoteNotificationResponse();
			MobageError out_err;
			try
			{
				response = JsonMapper.ToObject<MobageRemoteNotificationResponse> (utf8string);
				onSuccess(response);
			}
			catch
			{
				out_err = new MobageError();
				out_err.code = 500;
				out_err.description = "Internal error.";
				onError(out_err);
			}
			*/
		}
		
		public void GetonError(string key)
		{
			MobageError out_err;
			out_err = new MobageError();
			out_err.code = 500;
			out_err.description = key;
			onError(out_err);
			/*
			MobageError out_err;
			try
			{
				out_err = JsonMapper.ToObject<MobageError> (key);
				onError(out_err);
			}
			catch
			{
				out_err = new MobageError();
				out_err.code = 500;
				out_err.description = "Internal error.";
				onError(out_err);
			}
			*/
		}
	}
}

/*!
 * @abstract Callback listener interface for getRemoteNotificationsEnabled().
 */
namespace RemoteNotificationGetEnableCallBackLib
{
	/*!
	 * @abstract onSuccess Retrieves whether or not a currently logged in user can receive remote notifications.
	 */
	public delegate void OnSuccess(bool canBeNotified);

	/*!
	 * @abstract onError Callback interface that handles errors.
	 */
	public delegate void OnError(MobageError err);
	
	public class OnComplete{
		public OnComplete()
		{
		}
		
		private OnSuccess onSuccess;
		private OnError onError;

		public void SetSuccessCallBack(OnSuccess del)
        {
            onSuccess = del;
        }

		public void SetErrorCallBack(OnError del)
        {
            onError = del;
        }

		public void GetonSuccess(string utf8string)
		{
			bool canBeNotified = utf8string=="true"? true:false;
			onSuccess(canBeNotified);
		}
		
		public void GetonError(string key)
		{
			MobageError out_err;
			try
			{
				out_err = JsonMapper.ToObject<MobageError> (key);
				onError(out_err);
			}
			catch
			{
				out_err = new MobageError();
				out_err.code = 500;
				out_err.description = "Internal error.";
				onError(out_err);
			}
		}
	}
}

/*!
 * @abstract Callback listener interface for setRemoteNotificationsEnabled().
 */
namespace RemoteNotificationSetEnableCallBackLib
{
	/*!
	 * @abstract onSuccess Indicates the ability of a currently logged in user to recieve remote notifications was sucessfully set.
	 */
	public delegate void OnSuccess();

	/*!
	 * @abstract onError Callback interface that handles errors.
	 */
	public delegate void OnError(MobageError err);
	
	public class OnComplete{
		public OnComplete()
		{
		}
		
		private OnSuccess onSuccess;
		private OnError onError;

		public void SetSuccessCallBack(OnSuccess del)
        {
            onSuccess = del;
        }

		public void SetErrorCallBack(OnError del)
        {
            onError = del;
        }

		public void GetonSuccess(string utf8string)
		{
			onSuccess();
		}
		
		public void GetonError(string key)
		{
			MobageError out_err;
			try
			{
				out_err = JsonMapper.ToObject<MobageError> (key);
				onError(out_err);
			}
			catch
			{
				out_err = new MobageError();
				out_err.code = 500;
				out_err.description = "Internal error.";
				onError(out_err);
			}
		}
	}
}

/*!
 * @abstract Callback listener interface for handle a remote notification listener.
 */
namespace RemoteNotificationHandlerCallBackLib
{
	
	/*!
	 * @abstract Handler The listener receiving incoming remote notifications
	 * @param  RemoteNotificationPayload The Intent being received.
	 * <br/>When receiving, the parameters that guarantee each terminal is as follows..
	 * <br/>[Android]
	 * <br/> payload.message
	 * <br/> payload.collapseKey
	 * <br/> payload.style
	 * <br/> payload.iconUrl
	 * <br/> payload.extras
	 * <br/>[iOS]
	 * <br/> payload.badge
	 * <br/> payload.message
	 * <br/> payload.sound
	 * <br/> payload.extras
	 */
	public delegate void OnSuccess(MobageRemoteNotificationPayload payload);

	public class OnComplete {
		public OnComplete()
		{
		}
		
		private OnSuccess onSuccess;

		public void SetSuccessCallBack(OnSuccess del)
        {
            onSuccess = del;
        }

		public void GetonSuccess(string utf8string)
		{
			
			MobageRemoteNotificationPayload payload = new MobageRemoteNotificationPayload();
			try
			{
				payload = JsonMapper.ToObject<MobageRemoteNotificationPayload> (utf8string);
				onSuccess(payload);
			}
			catch
			{
				onSuccess(payload);
			}
		}
	}
}
	
/*!
 * @abstract Callback listener for DashBoard()
 */	
namespace AddDashBoardCallBackLib
{
	/*! */
	public delegate void OnDis();

	/*! */
	public delegate void OnError(MobageError err);
	
	public class OnComplete{
		public OnComplete()
		{
		}
		
		private OnDis onDis;
		private OnError onError;

		public void SetDismissCallBack(OnDis fnc)
        {
            onDis = fnc;
        }

		public void SetErrorCallBack(OnError fnc)
        {
            onError = fnc;
        }

		public void GetonDismiss(string key)
		{
			if(onDis != null) onDis();
		}
		
		public void GetonError(string key)
		{
			MobageError out_err = JsonMapper.ToObject<MobageError> (key);
			
			if(onError != null) onError(out_err);
		}
	}
}

namespace OnGetMobageVendorIdokCallBackLib
{
	/*!
	 * @abstract onSuccess Retrieves the requested leaderboard.
	 */
	public delegate void OnSuccess(string response);
	
	/*!
	 * @abstract onError Callback interface that handles errors.
	 */
	public delegate void OnError(MobageError err);
	
	public class OnComplete {
		
		public OnComplete ()
		{
			
		}
		private OnSuccess onSuccess;
		private OnError onError;
		
		public void SetSuccessCallBack(OnSuccess del)
		{
			onSuccess = del;
		}
		
		public void SetErrorCallBack(OnError del)
		{
			onError = del;
		}
		
		public void GetonSuccess(string utf8string)
		{
			MobageError out_err;
			try
			{
				string response = utf8string;
				onSuccess(response);
			}
			catch
			{
				out_err = new MobageError();
				out_err.code = 500;
				out_err.description = "Internal error.";
				onError(out_err);
			}
		}
		
		public void GetonError(string key)
		{
			MobageError out_err;
			try
			{
				out_err = JsonMapper.ToObject<MobageError>(key);
				onError(out_err);
			}
			catch
			{
				out_err = new MobageError();
				out_err.code  = 500;
				out_err.description = "Internal error.";
				onError(out_err);
			}
		}
	}
}