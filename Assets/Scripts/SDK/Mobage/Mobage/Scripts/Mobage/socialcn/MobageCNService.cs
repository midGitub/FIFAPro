/**
 * MobageCNService
 */

using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using MobageLitJson;


/*!
 * @abstract The CN's social local API for returning the LBS related information. 
 */
public class MobageCNService
{
	/*!
	 * @abstract set the visibility of mobage menu bar. 
	 *  This API is able to use at iOS side and android side, this function may use a NativeSDK API at JAVA.
	 */
	public static void setMenubarVisibility (string text)
	{
		MobageManager.setMenubarVisibility (text);
		return;
	}

	public static readonly string MENUBAR_LEFT_TOP = "0";
	public static readonly string MENUBAR_LEFT_BOTTOM = "1";
	public static readonly string MENUBAR_RIGHT_TOP = "2";
	public static readonly string MENUBAR_RIGHT_BOTTOM = "3";
	
	public static void ShowMenubar()
	{
		MobageManager.setMenubarVisibility("TRUE");
	}

	public static void ShowMenubar(string POSITION)
	{
		MobageManager.setMenubarPosition (POSITION);
		MobageManager.setMenubarVisibility("TRUE");
	}
	
	public static void HideMenubar()
	{
		MobageManager.setMenubarVisibility("FALSE");
	}


	/*!
	 * @abstract set the position of mobage menu bar. 
	 *  This API is able to use at iOS side and android side, this function may use a NativeSDK API at JAVA.
	 */
	public static void setMenubarPosition (string text)
	{
		MobageManager.setMenubarPosition(text);
		return;
	}
	

	/*!
	 * @abstract open Home Page
	 * @param onDismiss The callback interface to notify dismissing alert dialog
	 */
	public static void LaunchDashboardWithHomePage()
	{
		MobageManager.LaunchDashboardWithHomePage();
		return;
	}



	/*!
	 * @abstract get server environment. 
	 *  This API is able to use at iOS side. 
	 * @sandbox : return 0, production : return 1
	 */
	public static bool isProductionMode()
	{
	
		Debug.Log("@@isProductionMode:called: "+MobageManager.getServerModeFromConfig());
		
		if(1==MobageManager.getServerModeFromConfig())
			return true;
		else
			return false;
	}

};



/// <summary>
/// Mobage local notification.
/// </summary>
public class MobageLocalNotification
{
	
	private int _hour;
	private int _minute;
	private int _iconResourceId;
	private string _title;
	private string _text;
	
	//public int hoour { get; private set;}
	
	public int hour {
		get { return _hour; }
		set { _hour = value; }
	}
	public int minute {
		get { return _minute; }
		set { _minute = value + 1; }
	}
	public int iconResourceId {
		get { return _iconResourceId; }
		set { _iconResourceId = value; }
	}
	public string title {
		get { return _title; }
		set { _title = value; }
	}
	public string text {
		get { return _text; }
		set { _text = value; }
	}
	
	
	public void setStartTime(int _hour, int _minute){
		this._hour = _hour;
		this._minute = _minute;
	}
	
	public void setNotification(string _title, string _text){
		this._title = _title;
		this._text = _text;
	}
	
	public void setNotification(string _title, string _text, int _iconResourceId){
		this._title = _title;
		this._text = _text;
		this._iconResourceId = _iconResourceId;
	}

	
}





public class MobageLocalNotificationManager {

	public static bool setOnce(int requestCode, MobageLocalNotification notify){
		string json = null;
		JsonData jd = new JsonData ();
		jd ["requestCode"] = requestCode;
		jd ["notify"] = JsonMapper.ToObject(JsonMapper.ToJson(notify));
		
		json = JsonMapper.ToJson (jd);

		return MobageNative.SetOnce (json);
	}

	public static bool setRepeatingEveryDay(int requestCode, MobageLocalNotification notify){
		string json = null;
		JsonData jd = new JsonData ();
		jd ["requestCode"] = requestCode;
		jd ["notify"] = JsonMapper.ToObject(JsonMapper.ToJson(notify));
		
		json = JsonMapper.ToJson (jd);
		return MobageNative.SetRepeatingEveryDay (json);
	}

	public static bool setRepeatingEveryDay(int requestCode, MobageLocalNotification notify, int delayDay){
		string json = null;
		
		JsonData jd = new JsonData ();
		jd ["requestCode"] = requestCode;
		jd ["notify"] = JsonMapper.ToObject(JsonMapper.ToJson(notify));
		jd ["dalayDay"] = delayDay;
		json = JsonMapper.ToJson (jd);
		//Debug.Log (json);
		
		return MobageNative.SetRepeatingEveryDay (json);
	}

	public static bool setRepeatingDayOfWeek(int requestCode, int dayOfWeek, MobageLocalNotification notify){
		string json = null;
		JsonData jd = new JsonData ();
		jd ["requestCode"] = requestCode;
		jd ["dayOfWeek"] = dayOfWeek;
		jd ["notify"] = JsonMapper.ToObject(JsonMapper.ToJson (notify));
		json = JsonMapper.ToJson (jd);

		return MobageNative.SetRepeatingDayOfWeek (json);
	}

	public static bool removeLocalNotification(int requestCode){
		string json = null;
		JsonData jd = new JsonData ();
		jd ["requestCode"] = requestCode;
		json = JsonMapper.ToJson (jd);

		return MobageNative.RemoveLocalNotification (json);
	}

	public static int removeAllNotification(){

		return MobageNative.RemoveAllNotification ();
	}

}







