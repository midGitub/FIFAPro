
using System.Collections;



/*!
 * @abstract User Data object for People API
 * @discussion 
 */
public class MobageUser {

	private string id_ = "";
	private string displayName_ = "";
	private string nickname_ = "";
	private string aboutMe_ = "";
	//private Map<String, String> addresses; //not supported yet.
	private int age_;
	private int grade_;
	private string birthday_ = "";
	private string gender_ = "";
	private bool hasApp_ = false;
	//private ArrayList<String> interests; //not supported yet.
	private string thumbnailUrl_ = "";
	private string jobType_ = "";
	private string bloodType_ = "";
	private bool ageRestricted_ = false;
	private bool isVerified_ = false;
	private bool isFamous_ = false;
    
	/*!
	 * @abstract
	 */
	public string id
	{
		set{this.id_ = value;}
		get{return this.id_;}
	}

	/*!
	 * @abstract
	 */
	public string displayName
	{
		set{this.displayName_ = value;}
		get{return this.displayName_;}
	}

	/*!
	 * @abstract
	 */
	public string nickname
	{
		set{this.nickname_ = value;}
		get{return this.nickname_;}
	}

	/*!
	 * @abstract
	 */
	public string aboutMe
	{
		set{this.aboutMe_ = value;}
		get{return this.aboutMe_;}
	}

	/*!
	 * @abstract
	 */
	public int age
	{
		set{this.age_ = value;}
		get{return this.age_;}
	}
	
		/*!
	 * @abstract
	 */
	public int grade
	{
		set{this.grade_ = value;}
		get{return this.grade_;}
	}

	/*!
	 * @abstract
	 */
	public string birthday
	{
		set{this.birthday_ = value;}
		get{return this.birthday_;}
	}

	/*!
	 * @abstract
	 */
	public string gender
	{
		set{this.gender_ = value;}
		get{return this.gender_;}
	}

	/*!
	 * @abstract
	 */
	public bool hasApp 
	{
		set{this.hasApp_ = value;}
		get{return this.hasApp_;}
	}

	/*!
	 * @abstract
	 */
	public string thumbnailUrl 
	{
		set{this.thumbnailUrl_ = value;}
		get{return this.thumbnailUrl_;}
	}
	/*!
	 * @abstract
	 */
	public string jobType 
	{
		set{this.jobType_ = value;}
		get{return this.jobType_;}
	}

	/*!
	 * @abstract
	 */
	public string bloodType 
	{
		set{this.bloodType_ = value;}
		get{return this.bloodType_;}
	}

	/*!
	 * @abstract
	 */
	public bool ageRestricted 
	{
		set{this.ageRestricted_ = value;}
		get{return this.ageRestricted_;}
	}

	/*!
	 * @abstract
	 */
	public bool isVerified 
	{
		set{this.isVerified_ = value;}
		get{return this.isVerified_;}
	}
	
		/*!
	 * @abstract
	 */
	public bool isFamous
	{
		set{this.isFamous_ = value;}
		get{return this.isFamous_;}
	}

	public string encodebinary(){
		string chunk;

		chunk = "{";
		chunk += this.id_;
		chunk += ",";
		chunk += this.displayName;
		chunk += ",";
		chunk += this.nickname_;
		chunk += ",";
		chunk += this.aboutMe_;
		chunk += ",";
		chunk += this.age_;
		chunk += ",";
		chunk += this.grade_;
		chunk += ",";
		chunk += this.birthday_;
		chunk += ",";
		chunk += this.gender_;
		chunk += ",";
		chunk += "hasApp";
		chunk += ",";
		chunk += this.thumbnailUrl_;
		chunk += ",";
		chunk += this.jobType_;
		chunk += ",";
		chunk += this.bloodType_;
		chunk += ",";
		chunk += "ageRestricted";		
		chunk += ",";
		chunk += "isVerified";
		chunk += ",";
		chunk += "isFamous";
		chunk += "}";
		
		return chunk;
	}


}




