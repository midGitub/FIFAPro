// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections.Generic;


public class NetworkHttpRequest: MonoBehaviour, NetWorkRequestInterface
{
    public delegate void OnReturnNetWorkHandler(NetworkHttpRequest request);
    public OnReturnNetWorkHandler ReturnNetWorkHandler = null;

    public bool IsBusy
    {
        get
        {
            return isBusy;
        }
    }

    public void Request(string url, Dictionary<string, string> prams, string msgid, string respFuncName, OnRequestRespScript resp)
	{
        isBusy = true;
        StartCoroutine(doPost(url, prams, msgid, respFuncName, resp));
	}

    public void Request(string url, Dictionary<string, string> prams, string msgid, OnRequestResp resp)
    {
        isBusy = true;
        StartCoroutine(doPost(url, prams, msgid, resp));
    }
    
    public void Request(string url, string respFuncName, OnRequestRespScript resp)
	{
        isBusy = true;
		StartCoroutine(doGet(url, respFuncName, resp));
        
	}
    
    public void Request(string url, OnRequestResp resp)
    {
        isBusy = true;
        StartCoroutine(doGet(url, resp));
    }


    IEnumerator<WWW> doPost(string url, Dictionary<string, string> prams, string msgid, string respFuncName, OnRequestRespScript resp)
	{
		WWWForm form = new WWWForm();
        if (prams != null)
        {
            foreach (KeyValuePair<string, string> post_arg in prams)
            {
                form.AddField(post_arg.Key, post_arg.Value);
            }
        }
        form.AddField("msgid", msgid);
		
		WWW www = new WWW(url, form);
		yield return www;
		
		if (www.error != null)
		{
            Util.LogError("error is :" + www.error);
		}

        if(resp != null)
        {
            resp(this, www, respFuncName);
        }

        if (ReturnNetWorkHandler != null)
        {
            ReturnNetWorkHandler(this);
        }
		www.Dispose ();
	}

    IEnumerator<WWW> doPost(string url, Dictionary<string, string> prams, string msgid, OnRequestResp resp)
    {
        WWWForm form = new WWWForm();
        if (prams != null)
        {
            foreach (KeyValuePair<string, string> post_arg in prams)
            {
                form.AddField(post_arg.Key, post_arg.Value);
            }
        }
        form.AddField("msgid", msgid);
        WWW www = new WWW(url, form);
        yield return www;

        if (www.error != null)
        {
            Util.LogError("error is :" + www.error);
        }
        if(resp != null)
        {
            resp(this, www);
        }
        if (ReturnNetWorkHandler != null)
        {
            ReturnNetWorkHandler(this);
        }
        www.Dispose();
    }
	

    IEnumerator<WWW> doGet(string url, string respFuncName, OnRequestRespScript resp)
	{
		WWW www = new WWW (url);
		yield return www;
		
		if (www.error != null)
		{
            Util.LogError("error is :" + www.error);
			
		} 
        if (resp != null)
        {
            resp(this, www, respFuncName);
        }

        if (ReturnNetWorkHandler != null)
        {
            ReturnNetWorkHandler(this);
        }
		www.Dispose ();
	}
    
    IEnumerator<WWW> doGet(string url, OnRequestResp resp)
    {
        WWW www = new WWW(url);
        yield return www;

        if (www.error != null)
        {
            Util.LogError("error is :" + www.error);
        }
        if (resp != null)
        {
            resp(this, www);
        }

        if (ReturnNetWorkHandler != null)
        {
            ReturnNetWorkHandler(this);
        }
        www.Dispose();
    }

    void StopCoroutines()
    {
        StopAllCoroutines();
        isBusy = false;
    }

    bool isBusy = false;
}



