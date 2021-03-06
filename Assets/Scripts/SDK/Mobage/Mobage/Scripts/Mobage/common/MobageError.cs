/**
 * MBGError.h
 */

using System.Collections;


/*!
 * @abstract Error Data object for all Mobage API. 
 * @discussion The error information is equivalent to NativeSDK.
 */
public class MobageError {

	private int code_;
	private string description_;

	/*!
	 * @abstract Error Code::
	 * @discussion The error code is equivalent to NativeSDK.
	 * The following error code will be returned from the server.
	 * <br/>201	:	Created
	 * <br/>400	:	Bad Request
	 * <br/>401	:	Unauthorized
	 * <br/>403	:	Forbidden
	 * <br/>404	:	Not Found
	 * <br/>405	:	Method Not Allowed
	 * <br/>500	:	Internal Server Error
	 * <br/>503	:	Service Unavailable
	 * 
	 */
	public int code
	{
		set{this.code_ = value;}
		get{return this.code_;}
	}

	/*!
	 * @abstract Error Description
	 */
	public string description
	{
		set{this.description_ = value;}
		get{return this.description_;}
	}

}
