/**
 * MobageUnityBankDebit
 */


using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;



/*!
 * MobageUnityBankDebit class is an interface for purchasing in-game items.
 * @discussion A transaction begins by calling <I>createTransaction()</I>, which presents
 * a UI to the user. If the user follows through with a purchase in the UI, <I>createTransaction()</I> changes its state from 
 * <I>new</I> to <I>authorized</I>. Once <I>createTransaction()</I> executes its callback, the game should call 
 * <I>openTransaction()</I>, which changes the state from <I>authorized</I> to <I>open</I> and puts funds into escrow. 
 * At this point, the game/game server should deliver the purchased items. Once the items have been delivered, the game should call   
 * <I>closeTransaction()</I>. If there is a problem at any point in the transaction lifecycle, you may call <I>cancelTransaction()</I>,
 * which sets the transaction state to <I>canceled</I> and restores funds from escrow back to the user.
 */
public class MobageBankDebit {

	/*!
	 * @abstract Creates a transaction with the <I>transaction.state</I> set to <I>new</I>.
	 * @discussion Mobage presents a user interface that prompts the user to confirm the transaction.
	 * If the user decides not to proceed with the transaction, the callback error will indicate "user canceled."
	 * In the client-only flow, it checks inventory and sets the state to <I>authorized</I>.
	 * @param billingItems The billing items for the transaction. <br/><b>Note:</b> The array is limited to one item for this release.
	 * @param onSuccess callback Retrieves the transaction details.
	 * @param onError The callback interface that handles errors.
	 *
	 */
	public static void createTransaction( List<MobageBillingItem> items, string comment, 
	                                     BankTranDlgCmpCallBackLib.OnSuccess onSuccess,
	                                     BankTranDlgCmpCallBackLib.OnError onError,
	                                     BankTranDlgCmpCallBackLib.OnCancel onCancel )
	{
		if(items == null || items.Count == 0) {
			MobageError err = new MobageError();
			err.description = "BillingItem is empty";
			err.code = 400;
			onError(err);
		}
		
		MobageManager.createTransaction(items, comment, onSuccess, onError, onCancel);
		return;
	}

	/*!
	 * @param onSuccess callback Retrieves the transaction details.
	 * @param onError The callback interface that handles errors.
	 */
	public static void openTransaction( string transactionId, 
	                                     BankTranCmpCallBackLib.OnSuccess onSuccess, 
	                                     BankTranCmpCallBackLib.OnError onError )	                                   
	{
		MobageManager.openTransaction(transactionId, onSuccess, onError);
		return;
	}

	/*!
	 * @param onSuccess callback Retrieves the transaction details.
	 * @param onError The callback interface that handles errors.
	 */
	public static void closeTransaction( string transactionId, 
	                                     BankTranCmpCallBackLib.OnSuccess onSuccess, 
	                                     BankTranCmpCallBackLib.OnError onError )
	{
		MobageManager.closeTransaction(transactionId, onSuccess, onError);
		return;
	}
	
	/*!
	 * @abstract Continues processing a transaction in the <I>new</I> state created in the Mobage platform server by the game server.
	 * @discussion Checks the inventory, and transitions the <I>transaction.state</I> from <I>new</I> to <I>authorized</I>.
	 * Then, it places funds into escrow. The <I>transaction.state</I> transitions from <I>authorized</I> to <I>open</I>, which
	 * indicates the game server needs to deliver the virtual item.
	 * @param transactionId The <I>transactionId</I> identifying this transaction.
	 * @param onSuccess Retrieves the transaction details.
	 * @param onError The callback interface that handles errors.
	 */
	public static void continueTransaction( string transactionId, 
	                                     BankTranDlgCmpCallBackLib.OnSuccess onSuccess,
	                                     BankTranDlgCmpCallBackLib.OnError onError,
	                                     BankTranDlgCmpCallBackLib.OnCancel onCancel )
	{
		MobageManager.continueTransaction(transactionId, onSuccess, onError, onCancel);
		return;
	}

	/*!
	 * @abstract Cancels the transaction, which indicates the transaction was canceled or the virtual item could not be delivered and
	 * funds need to be returned.
	 * @discussion The <I>transaction.state</I> transitions to <I>canceled.</I>
	 * @param transactionId The <I>transactionId</I> identifying this transaction.
	 * @param onSuccess Retrieves the transaction details.
	 * @param onError The callback interface that handles errors.
	 */
	public static void cancelTransaction( string transactionId, 
	                                     BankTranCmpCallBackLib.OnSuccess onSuccess, 
	                                     BankTranCmpCallBackLib.OnError onError )
	{
		MobageManager.cancelTransaction(transactionId, onSuccess, onError);
		return;
	}
	
	/*!
	 * @abstract Retrieves the transaction corresponding to the given transaction ID parameter.
	 * @param transactionId The <I>transactionId</I> identifying this transaction.
	 * @param onSuccess Retrieves the transaction details.
	 * @param onError The callback interface that handles errors.
	 */
	public static void getTransaction( string transactionId,
	                                     BankTranCmpCallBackLib.OnSuccess onSuccess, 
	                                     BankTranCmpCallBackLib.OnError onError )
	{
		MobageManager.getTransaction(transactionId, onSuccess, onError);
		return;
	}
	
	/*!
	 * @abstract **Not Supported for now**  Retrieves the user's transactions where the state is <I>new</I>, <I>authorized</I> or <I>open</I>.
	 * @param onComplete Retrieves the transaction details.
	 * @param onError The callback interface that handles errors.
	 */
	public static void getPendingTransactions(BankTranCmpCallBackLib.OnSuccess onSuccess, 
	                                          BankTranCmpCallBackLib.OnError onError )
	{
		MobageManager.getPendingTransactions(onSuccess, onError);
		return;
	}
	

}
