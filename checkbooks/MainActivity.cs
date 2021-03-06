using Android.App;
using Android.Widget;
using Android.OS;
using System;
using SQLite;
using Android.Views.InputMethods;
using System.Collections.Generic;
using com.refractored.fab;

namespace checkbooks
{
	[Activity(Label = "checkbooks", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		
		protected ListView _transactionListView;
		protected TransactionAdapter _transactionAdapter;
		protected FloatingActionButton _addTransactionFab;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			_transactionListView = FindViewById<ListView>(Resource.Id.RecentActivity);

			_addTransactionFab = FindViewById<FloatingActionButton>(Resource.Id.AddTransactionFab);
			_addTransactionFab.AttachToListView(_transactionListView);

			// TODO: Add progress bar to track toward monthly limit.

			_addTransactionFab.Click += delegate {
				StartActivity(typeof(AddTransaction));
			};
			/* {
					Transaction transaction = new Transaction
					{
						Amount = Convert.ToDecimal(_amount.Text),
						Type = _typeSpinner.SelectedItem.ToString(),
						Amount = (decimal)25.52,
						Type = "Test",
						Date = DateTime.Now
					};

					// var shit = Android.Content.Context.InputMethodService;
					// InputMethodManager imm = (InputMethodManager)GetSystemService(shit);
					// imm.HideSoftInputFromWindow(_amount.WindowToken, HideSoftInputFlags.None);

					_transactionAdapter.Insert(transaction, 0);
					_transactionListView.SmoothScrollToPosition(0);
					Toast.MakeText(this, "Transaction added!", ToastLength.Short).Show();
					// _amount.SetText("", TextView.BufferType.Editable);
			}; // TODO: Move this into a function. And make prettier. Current display hideous. TODO: Improve logic for type/subtype
*/
			// I think this needs to somehow get all the.. oh! From the database.
			string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			_transactionAdapter = new TransactionAdapter(this, System.IO.Path.Combine(folder, Resources.GetString(Resource.String.transaction_db)));
			_transactionListView.Adapter = _transactionAdapter;
		}
	}
}

