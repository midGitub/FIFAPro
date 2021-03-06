/**
 * MBGResult.h
 */

using System.Collections;



/*!
 * @abstract
 */
public class MobageResult {

	public int start;
	public int count;
	public int total;


	/*!
	 * @abstract
	 */
	public void setStart(int val){
		start = val;
	}
		
	/*!
	 * @abstract
	 */
	public void setCount(int val){
		count = val;
	}

	/*!
	 * @abstract
	 */
	public void setTotal(int val){
		total = val;
	}

	/*!
	 * @abstract
	 */
	public int getStart(){
		return start;
	}
		
	/*!
	 * @abstract
	 */
	public int getCount(){
		return count;
	}

	/*!
	 * @abstract
	 */
	public int getTotal(){
		return total;
	}
	
}

