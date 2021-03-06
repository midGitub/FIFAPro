/**
 * MobageUnityBankItem
 */


using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

/*!
 * @abstract represents Item Data for creating Transaction
 * @discussion 
 */
public struct MobageItemData {
	private string id_;
	private string name_;
	private int    price_;
	private string description_;
	private string imageUrl_;

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
	public string name
	{
		set{this.name_ = value;}
		get{return this.name_;}
	}
	/*!
	 * @abstract
	 */
	public int price
	{
		set{this.price_ = value;}
		get{return this.price_;}
	}
	/*!
	 * @abstract
	 */
	public string description
	{
		set{this.description_ = value;}
		get{return this.description_;}
	}
	/*!
	 * @abstract
	 */
	public string imageUrl
	{
		set{this.imageUrl_ = value;}
		get{return this.imageUrl_;}
	}
}

/*!
 * @abstract represents Billing Item for creating Transaction
 * @discussion 
 */
public struct MobageBillingItem {
	private MobageItemData item_;
	private int quantity_;

	/*!
	 * @abstract
	 */
	public MobageItemData item
	{
		set{this.item_ = value;}
		get{return this.item_;}
	}
	/*!
	 * @abstract
	 */
	public int quantity
	{
		set{this.quantity_ = value;}
		get{return this.quantity_;}
	}
}

/*!
 * @abstract Transaction Data Object
 * @discussion 
 */
public struct MobageTransaction {
	private string id_;
	private List<MobageBillingItem> items_;
	private string comment_;
	private string state_;
	private string published_;
	private string updated_;

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
	public List<MobageBillingItem> items
	{
		set{this.items_ = value;}
		get{return this.items_;}
	}
	/*!
	 * @abstract
	 */
	public string comment
	{
		set{this.comment_ = value;}
		get{return this.comment_;}
	}
	/*!
	 * @abstract
	 */
	public string state
	{
		set{this.state_ = value;}
		get{return this.state_;}
	}
	/*!
	 * @abstract
	 */
	public string published
	{
		set{this.published_ = value;}
		get{return this.published_;}
	}
	/*!
	 * @abstract
	 */
	public string updated
	{
		set{this.updated_ = value;}
		get{return this.updated_;}
	}
}

