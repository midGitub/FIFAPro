/**
 * MobageUnityService
 */

using System.Collections;
using System.Runtime.InteropServices;



/*!
 * @abstract Social features using UI Components
 * @discussion 
 */
public class MobageService
{

	/*!
	 * @abstract shows Bank dialog to purchase game currency
	 * @param onDismiss The callback interface to notify dismissing Webview dialog
	 */
	public static void showBankUI(DialogCompleteCallBackLib.onDismiss onDismiss)
	{
		MobageManager.showBankUI(onDismiss);
		return;
	}
	
	/*!
	 * @abstract market code
	 * @param onDismiss The callback interface to notify dismissing Webview dialog
	 */
	public static void getBalance(BalanceButtonCallBackLib.OnSuccess onSuccess, 
	                              BalanceButtonCallBackLib.OnError onError ) 
	{
		MobageManager.getBalance(onSuccess, onError);
		return;
	}
	


	/// <summary>
	/// 在编辑器中始终返回00000000
	/// </summary>
	public static string GetAffcode()
	{
		return Proxy.GetAffcode.Invock ();
	}

	/// <summary>
	/// 只在安卓真机上有效
	/// </summary>
	public static void OpenAccountRechargePage()
	{
		Proxy.OpenAccountRechargePage.Invock ();
	}

	/// <summary>
	/// 只在安卓真机上有效
	/// </summary>
	public static void OpenCustomerServicePage()
	{
		Proxy.OpenCustomerServicePage.Invock ();
	}

	/// <summary>
	/// 是否允许绘制客服按钮
	/// ※ 某些渠道不允许出现梦包谷客服，这个API的返回值可以在服务器进行配置
	/// </summary>
	public static bool HasCustomerService()
	{
		return Proxy.HasCustomerServicePage.Invock ();
	}

	/// <summary>
	/// 设置额外信息/传入统计信息
	/// ※ 游戏需要在指定的场合调用这个接口，传入指定数据，这些数据将会传回服务器进行统计
	/// </summary>
	public static void setExtraData(string ext)
	{
		Proxy.setExtraData.Invock (ext);
	}
	/// <summary>
	/// 当前渠道是否有论坛
	/// </summary>
	public static bool HasForum()
	{
		return MobageNative.HasForum ();
	}

	/// <summary>
	/// 显示当前渠道的论坛, 只在安卓真机上有效
	/// </summary>
	public static void ShowForum()
	{
		MobageNative.ShowForum ();
	}

	/// <summary>
	/// Logout mobage account
	/// </summary>
	public static void Logout()
	{
		MobageNative.Logout ();
	}

	public static void GetFacebookUser(Proxy.GetFacebookUser.Callback callback)
	{
		Proxy.GetFacebookUser.Invock(callback);
	}
};

public class Balance{
	public string balance{ get; private set;}
	public string limitation {get; private set;}
	public string state {get; private set;}
	
	public Balance(string balance, string limitation, string state){
		this.balance = balance;
		this.limitation = limitation;
		this.state = state;
	}
}
