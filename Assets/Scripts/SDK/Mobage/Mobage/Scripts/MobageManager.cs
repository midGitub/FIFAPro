using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Threading;
using Proxy;
using MobageLitJson;

public class MobageManager {

	public static string AndroidInterfaceVersion = "A6";
	public static string IOSInterfaceVersion = "I5";

	// Timer thread for Mobage::Tick()
	private static System.Threading.Timer tm;
    public static void registerTick() {
		//在当前版本中不用
		/*
#if UNITY_IPHONE || UNITY_EDITOR
        tm = new Timer(TimerCallbackMethod, null, 0, 400);
#endif
*/
	}

    public static void registerTickStop() {
        tm.Dispose();
		tm = null;
	}
	
	private static void TimerCallbackMethod(object state) {
		MobageNative.Tick();
    }
	
	public static int getServerModeFromConfig() {		
		return MobageNative.GetServerModeFromConfig();
	}
	

	public static void addLoginListener(AddLoginCallBackLib.onLoginComplete onComp, 
	                                    AddLoginCallBackLib.onLoginRequired onRequired, 
	                                    AddLoginCallBackLib.onCancel onCancel, 
	                                    AddLoginCallBackLib.onError onError) {

		MobageCallbackManager.loginLib.SetCompCallBack(onComp);
		MobageCallbackManager.loginLib.SetReqedCallBack(onRequired);
		MobageCallbackManager.loginLib.SetCancelCallBack(onCancel);
		MobageCallbackManager.loginLib.SetErrorCallBack(onError);

		
#if UNITY_EDITOR || UNITY_STANDALONE
		MobageNative.AddLoginListener( );
#endif
		return;
	}

	//!!
	public static void addSwitchAccountListenner(SwitchAccountProxy.OnSwitchAccount onSwitchAccount)
	{
		SwitchAccountProxy.CallBack_onSwitchAccount = onSwitchAccount;
	}

	public static void addLogoutListenner(LogoutListener.OnLogoutComplete onLogoutComplete)
	{
		MobageCallbackManager.logoutListenner.CallBack_onLogoutComplete = onLogoutComplete;
	}
	
	public static void addDashBoardListener(AddDashBoardCallBackLib.OnDis onDis)
	{
		MobageCallbackManager.dashBoardLib.SetDismissCallBack(onDis);
	}

	//!!
	public static void addXPromotionListenner(XPromotionListener.DidShow didShow, XPromotionListener.DidClose didClose)
	{
		XPromotionListener.CallBack_DidShow = didShow;
		XPromotionListener.CallBack_DidClose = didClose;
		//native not called
		//XPromotionListener.CallBack_DidClick = didClick;
	}


	public static void initialize(int region, int serverMode, string consumerKey, string consumerSecret, string appId) {
		MLog.i ("MobageManager", "initialize");
		HostConfig.GetInstance ().Init (serverMode, region);
#if UNITY_ANDROID && !UNITY_EDITOR
		PJson json = new PJson ();
		json ["region"] = region;
		json ["serverMode"] = serverMode;
		json ["consumerKey"] = consumerKey;
		json ["consumerSecret"] = consumerSecret;
		json ["appId"] = appId;

		MobageNative.Initialization(json.ToString());		
#else
		MobageNative.Initialization(region, serverMode, consumerKey, consumerSecret, appId);				
#endif
	}
	


	
	public static void getFriends(string userId, List<string> fields, MobageOption option, 
	                              PeopleUsersCallBackLib.OnSuccess onSuccess, 
	                              PeopleUsersCallBackLib.OnError onError) {

		MobageCallbackManager.usrsLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.usrsLib.SetErrorCallBack(onError);

		string chunk = MobageDispatcher.getfriends(userId, fields, option);

		MobageNative.SocialRequestDispatcherGetFriends(chunk);

		return;
	}

	public static void getFriendsWithGame(string userId, List<string> fields, MobageOption option, 
	                                      PeopleUsersCallBackLib.OnSuccess onSuccess, 
	                                      PeopleUsersCallBackLib.OnError onError) {

		MobageCallbackManager.usrsLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.usrsLib.SetErrorCallBack(onError);

		string chunk = MobageDispatcher.getfriendswithGame(userId, fields, option);

		MobageNative.SocialRequestDispatcherGetFriendsWithGame(chunk);

		return;
	}



	public static void getUsers(List<string> userIds, List<string> fields, 
	                            PeopleUsersCallBackLib.OnSuccess onSuccess, 
	                            PeopleUsersCallBackLib.OnError onError) {

		MobageCallbackManager.usrsLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.usrsLib.SetErrorCallBack(onError);

		string chunk = MobageDispatcher.getusers(userIds, fields); 

		MobageNative.SocialRequestDispatcherGetUsers(chunk);

		return;
	}

	// BlackList
	public static void checkBlacklist(string userId, string targetUserId, MobageOption options, 
	                                  BlacklistCallBackLib.OnSuccess onSuccess, 
	                                  BlacklistCallBackLib.OnError onError) {

		MobageCallbackManager.blLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.blLib.SetErrorCallBack(onError);

		string chunk = MobageDispatcher.checkblacklist(userId, targetUserId, options);
		
		MobageNative.SocialRequestDispatcherCheckBlackList(chunk);

		return;
	}

	public static void showBankUI(DialogCompleteCallBackLib.onDismiss onDismiss) {

		MobageCallbackManager.dlgdisLib.SetSuccessCallBack(onDismiss);

		MobageNative.SocialRequestDispatchershowBankUI();

		return;
	}
	
	// Auth
	public static void authorizeToken(string token, 
	                                  AuthCallBackLib.OnSuccess onSuccess, 
	                                  AuthCallBackLib.OnError onError) {

		MobageCallbackManager.authLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.authLib.SetErrorCallBack(onError);

		MobageNative.SocialRequestDispatcherAuth(token);

		return;
	}

	
	// Bank
	public static void getItem(string itemId, 
	                           BankInventryCallBackLib.OnSuccess onSuccess, 
	                           BankInventryCallBackLib.OnError onError) {

		MobageCallbackManager.bnkinvLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.bnkinvLib.SetErrorCallBack(onError);

		MobageNative.SocialRequestDispatcherBankInventorygetItem(itemId);

		return;
	}
	
	public static void createTransaction(List<MobageBillingItem> items, string comment, 
	                                     BankTranDlgCmpCallBackLib.OnSuccess onSuccess,
	                                     BankTranDlgCmpCallBackLib.OnError onError,
	                                     BankTranDlgCmpCallBackLib.OnCancel onCancel ){

		MobageCallbackManager.bnktrncmpLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.bnktrncmpLib.SetErrorCallBack(onError);
		MobageCallbackManager.bnktrncmpLib.SetCancelCallBack(onCancel);

		string chunk = MobageDispatcher.createtransaction(items, comment);

		MobageNative.SocialRequestDispatchercreateTransaction(chunk);

		return;
	}

	public static void continueTransaction(string transactionId, 
	                                     BankTranDlgCmpCallBackLib.OnSuccess onSuccess,
	                                     BankTranDlgCmpCallBackLib.OnError onError,
	                                     BankTranDlgCmpCallBackLib.OnCancel onCancel ){

		MobageCallbackManager.bnktrncmpLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.bnktrncmpLib.SetErrorCallBack(onError);
		MobageCallbackManager.bnktrncmpLib.SetCancelCallBack(onCancel);

		MobageNative.SocialRequestDispatchercontinueTransaction(transactionId);

		return;
	}
	
	public static void cancelTransaction(string transactionId, 
	                                     BankTranCmpCallBackLib.OnSuccess onSuccess, 
	                                     BankTranCmpCallBackLib.OnError onError ) {

		MobageCallbackManager.bnktrnLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.bnktrnLib.SetErrorCallBack(onError);

		MobageNative.SocialRequestDispatchercancelTransaction(transactionId);

		return;
	}
	
	public static void closeTransaction(string transactionId, 
	                                     BankTranCmpCallBackLib.OnSuccess onSuccess, 
	                                     BankTranCmpCallBackLib.OnError onError ) {

		MobageCallbackManager.bnktrnLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.bnktrnLib.SetErrorCallBack(onError);

		MobageNative.SocialRequestDispatchercloseTransaction(transactionId);

		return;
	}
	
	public static void getTransaction(string transactionId, 
	                                  BankTranCmpCallBackLib.OnSuccess onSuccess, 
	                                  BankTranCmpCallBackLib.OnError onError ) {

		MobageCallbackManager.bnktrnLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.bnktrnLib.SetErrorCallBack(onError);

		MobageNative.SocialRequestDispatchergetTransaction(transactionId);

		return;
	}
	
	public static void getPendingTransactions( BankTranCmpCallBackLib.OnSuccess onSuccess, 
	                                          BankTranCmpCallBackLib.OnError onError ) {

		MobageCallbackManager.bnktrnLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.bnktrnLib.SetErrorCallBack(onError);

		MobageNative.SocialRequestDispatchergetPendingTransactions();

		return;
	}
	
	public static void openTransaction(string transactionId, 
	                                     BankTranCmpCallBackLib.OnSuccess onSuccess, 
	                                     BankTranCmpCallBackLib.OnError onError ) {


		MobageCallbackManager.bnktrnLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.bnktrnLib.SetErrorCallBack(onError);

		MobageNative.SocialRequestDispatcheropenTransaction(transactionId);

		return;
	}
	
	public static void getBalance(BalanceButtonCallBackLib.OnSuccess onSuccess, 
	                              BalanceButtonCallBackLib.OnError onError ) {

		MobageCallbackManager.balancebtnLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.balancebtnLib.SetErrorCallBack(onError);

		MobageNative.SocialRequestDispatchergetBalance();
		return;
	}

	/*
	public static void ShowLogoutDialog(LogoutCallBackLib.OnSuccess onSuccess, 
	                          LogoutCallBackLib.OnCancel onCancel) {

		MobageCallbackManager.logoutLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.logoutLib.SetCancelCallBack(onCancel);

		MobageNative.ShowLogoutDialog();
		return;
	}
	*/

	public static void Logout() {
			
		MobageNative.Logout ();
		return;
	}

	// Remote Notification
	public static void Pushsend( string recipientId, 
								MobageRemoteNotificationPayload data,
	                            RemoteNotificationSendCallBackLib.OnSuccess onSuccess, 
	                            RemoteNotificationSendCallBackLib.OnError onError ) {

		MobageCallbackManager.pushsendLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.pushsendLib.SetErrorCallBack(onError);

#if UNITY_IOS && !UNITY_EDITOR
		PJson json = new PJson();
		json["recipientId"] = recipientId;
		json["data"] = data.getPJson();
		string chunk = json.ToJString();
#else
		string chunk = MobageDispatcher.send(recipientId, data);

#endif
		MobageNative.SocialRequestDispatcherPushSend(chunk);
		
		return;
	}

	public static void getRemoteNotificationsEnabled( 
	                              RemoteNotificationGetEnableCallBackLib.OnSuccess onSuccess, 
	                              RemoteNotificationGetEnableCallBackLib.OnError onError ) {
		  
		MobageCallbackManager.pushgetLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.pushgetLib.SetErrorCallBack(onError);
		MobageNative.SocialRequestDispatchergetPushEnabled();
		return;
	}


	public static void setRemoteNotificationsEnabled( bool enabled, 
	                              RemoteNotificationSetEnableCallBackLib.OnSuccess onSuccess, 
	                              RemoteNotificationSetEnableCallBackLib.OnError onError ) {

		MobageCallbackManager.pushsetLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.pushsetLib.SetErrorCallBack(onError);

		string chunk = MobageDispatcher.setRemoteNotificationsEnabled(enabled);

		MobageNative.SocialRequestDispatchersetPushEnabled(chunk);

		return;
	}

	
	public static void setMenubarVisibility(string text) {
		MobageNative.SetMenubarVisibility(text);
		return;
	}
	
	public static void setMenubarPosition(string text) {
		
		MobageNative.SetMenubarPosition(text);
		return;
	}


	public static void getMobageVendorId(OnGetMobageVendorIdokCallBackLib.OnSuccess onSuccess, OnGetMobageVendorIdokCallBackLib.OnError onError)
	{
		MobageCallbackManager.VendorIdLib.SetSuccessCallBack(onSuccess);
		MobageCallbackManager.VendorIdLib.SetErrorCallBack(onError);
		MobageNative.GetMobageVendorId();
		return;
	}
	

	public static void checkLoginStatus() {
		MobageNative.CheckLoginStatus();
	}

	public static void LaunchDashboardWithHomePage() {
		//MobageCallbackManager.dlgdisLib.SetSuccessCallBack(onDismiss);
		MobageNative.LaunchDashboardWithHomePage();
		return;
	}

	public static void QuitSDK()
	{
		MobageNative.QuitSDK ();
	}

	//安卓平台
	#if UNITY_ANDROID
	public static void testPhotoUpload()
	{
		
		MobageNative.TestPhotoUpload ();
		return;
	}
	#endif

	//IOS平台


	//TODO: 返回M币，在下个发布的安卓版本中修改
	//货币单位
	public static void GetvcNameStr() {
	
#if UNITY_IPHONE 
		MobageNative.GetvcNameStr();
#endif
	}
	

	//IOS或其他平台
	#if UNITY_IPHONE || (!UNITY_ANDROID && !UNITY_IPHONE)
	public static void remoteNotificationListener( RemoteNotificationHandlerCallBackLib.OnSuccess onHandler ) {
		MobageCallbackManager.handlerLib.SetSuccessCallBack(onHandler);
		MobageNative.SocialRequestDispatcherNotificationListener( );
		return;
	}
	#endif
	

}



/*
 * CallBackRegister
 */

public class MobageCallbackManager {

	//LogoutListenner
	public static LogoutListener logoutListenner = new LogoutListener();

	// AddLogin
	public static AddLoginCallBackLib.OnComplete loginLib = new AddLoginCallBackLib.OnComplete();

	// Authentize
	public static AuthCallBackLib.OnComplete authLib = new AuthCallBackLib.OnComplete();
	// BlackList
	public static BlacklistCallBackLib.OnComplete blLib = new BlacklistCallBackLib.OnComplete();
	// People
	//public static PeopleUserCallBackLib.OnComplete usrLib = new PeopleUserCallBackLib.OnComplete();
	public static PeopleUsersCallBackLib.OnComplete usrsLib = new PeopleUsersCallBackLib.OnComplete();

	// JPSocial		
	public static DialogCompleteCallBackLib.OnComplete dlgdisLib = new DialogCompleteCallBackLib.OnComplete();

	// Bank Inventry
	public static BankInventryCallBackLib.OnComplete bnkinvLib = new BankInventryCallBackLib.OnComplete();
	// Bank Debit
	public static BankTranDlgCmpCallBackLib.OnComplete bnktrncmpLib = new BankTranDlgCmpCallBackLib.OnComplete();

	public static BankTranCmpCallBackLib.OnComplete bnktrnLib = new BankTranCmpCallBackLib.OnComplete();
	//get balance use this
	public static BalanceButtonCallBackLib.OnComplete balancebtnLib = new BalanceButtonCallBackLib.OnComplete();

	// MarketCode
	public static MarketCodeCallBackLib.OnComplete marketLib = new MarketCodeCallBackLib.OnComplete();

	// Push Notification
	public static RemoteNotificationSendCallBackLib.OnComplete pushsendLib = new RemoteNotificationSendCallBackLib.OnComplete();

	public static RemoteNotificationGetEnableCallBackLib.OnComplete pushgetLib = new RemoteNotificationGetEnableCallBackLib.OnComplete();

	public static RemoteNotificationSetEnableCallBackLib.OnComplete pushsetLib = new RemoteNotificationSetEnableCallBackLib.OnComplete();

	public static RemoteNotificationHandlerCallBackLib.OnComplete handlerLib = new RemoteNotificationHandlerCallBackLib.OnComplete();
	
	// CNSocial
	// AddDashBoard
	public static AddDashBoardCallBackLib.OnComplete dashBoardLib = new AddDashBoardCallBackLib.OnComplete();

	public static OnGetMobageVendorIdokCallBackLib.OnComplete VendorIdLib = new OnGetMobageVendorIdokCallBackLib.OnComplete();
}


