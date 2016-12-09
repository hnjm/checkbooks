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
		string createDatabase(string path)
		{
			try
			{
				var connection = new SQLiteAsyncConnection(path);
				{
					connection.CreateTableAsync<Transaction>();
					return "Database created";
				}
			}
			catch (SQLiteException ex)
			{
				return ex.Message;
			}
		}

		protected ListView _transactionListView;
		protected List<string> mItems;
		// protected Button _addTransaction;
		protected EditText _amount;
		protected TransactionAdapter _transactionAdapter;
		protected ArrayAdapter _typeAdapter;
		protected Spinner _typeSpinner;
		protected FloatingActionButton _addTransactionFab;
		protected ProgressBar _budgetProgress;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// create db connection?
			string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

			// TODO: Figure out if it's better to keep a type + subtype, or infer overarching type on the subtype.
			// TODO: Should the user be able to select + or -? They're not gonna get Groceries as a +. 

			// _addTransactionFab = FindViewById<Button>(Resource.Id.AddTransaction);
			_transactionListView = FindViewById<ListView>(Resource.Id.RecentActivity);
			// _typeSpinner = FindViewById<Spinner>(Resource.Id.TypeSpinner);
			// _amount = FindViewById<EditText>(Resource.Id.Amount);

			_addTransactionFab = FindViewById<FloatingActionButton>(Resource.Id.AddTransactionFab);
			_addTransactionFab.AttachToListView(_transactionListView);

			_budgetProgress = FindViewById<ProgressBar>(Resource.Id.BudgetProgress);

			// _typeAdapter = ArrayAdapter.CreateFromResource(
			// this, Resource.Array.subtype_array, Android.Resource.Layout.SimpleSpinnerItem);

			// _typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			// _typeSpinner.Adapter = _typeAdapter;
			// TODO: Change this to a button that leads into a new activity. Maybe make circular for grins/giggles.
			// TODO: Add progress bar to track toward monthly limit.

			_budgetProgress.SetProgress(50, true);

			_addTransactionFab.Click += (sender, e) =>
			{
					Transaction transaction = new Transaction
					{
						/* Amount = Convert.ToDecimal(_amount.Text),
						Type = _typeSpinner.SelectedItem.ToString(), */
						Amount = (decimal)25.52,
						Type = "Test",
						Date = DateTime.Now
					};

					var shit = Android.Content.Context.InputMethodService;
					InputMethodManager imm = (InputMethodManager)GetSystemService(shit);
					// imm.HideSoftInputFromWindow(_amount.WindowToken, HideSoftInputFlags.None);

					_transactionAdapter.Insert(transaction, 0);
					_transactionListView.SmoothScrollToPosition(0);
					Toast.MakeText(this, "Transaction added!", ToastLength.Short).Show();
					// _amount.SetText("", TextView.BufferType.Editable);
			}; // TODO: Move this into a function. And make prettier. Current display hideous. TODO: Improve logic for type/subtype

			// I think this needs to somehow get all the.. oh! From the database.
			_transactionAdapter = new TransactionAdapter(this, System.IO.Path.Combine(folder, Resources.GetString(Resource.String.transaction_db)));
			_transactionListView.Adapter = _transactionAdapter;
		}
	}
}

