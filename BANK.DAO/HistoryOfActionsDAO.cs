using System;
using System.Globalization;
using System.Transactions;
using BANK.Model;
using BANK.Model.Enums;
namespace BANK.DAO
{
    public class HistoryOfActionsDAO : IHistoryOfActionsDAO
    {
        List<HistoryOfActions> historyOfActions { get; set; } = new();
        private const string HistoryOfActionsPath = "historyOfAction.csv";


        public void save()
        {
            //var historyOfActions = new List<HistoryOfActions>
            //{
            //    new HistoryOfActions("BB", "ID2", DateTime.Today, ActionType.CreateClient, "ID1","MAC"),
            //    new HistoryOfActions("ŠŠ","IDD2",DateTime.Now,ActionType.ApprovedTransaction,"ID1","DOM"),
                

            //};

            string path = Path.Combine(Environment.CurrentDirectory, HistoryOfActionsPath);

            Console.WriteLine($"{path}");

            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine("Id, AccountId, ActionDate, ActionType, EmployeeId, BankAccountId");

                foreach (var historyAction in historyOfActions)
                {
                    writer.WriteLine($"\"{historyAction.Id}\",\"{historyAction.AccountId}\",{historyAction.ActionDate},{historyAction.ActionType},{historyAction.EmployeeId},{historyAction.BankAccountId}");
                }
            }
        }


        public void load()
        {
            List<HistoryOfActions> historyOfActions = new List<HistoryOfActions>();

            if (!File.Exists(HistoryOfActionsPath))
            {
                Console.WriteLine("The history of actions file does not exist.");
                return;
            }

            using (var reader = new StreamReader(HistoryOfActionsPath))
            {

                reader.ReadLine();

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    var historyofactions = new HistoryOfActions(
                        values[0].Trim('\"'),
                        values[1].Trim('\"'),
                        DateTime.Parse(values[1], CultureInfo.InvariantCulture),
                        (ActionType)Enum.Parse(typeof(ActionType), values[3]),
                        values[4],
                        values[5]
                        
                    );

                    historyOfActions.Add(historyofactions);
                }
            }

            
        }


        public HistoryOfActions? getHistoryOfActionsById(string id)
        {
            return historyOfActions.Where(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public List<HistoryOfActions> getAllHistoryOfAction()
        {
            return historyOfActions;
        }

        public HistoryOfActions? removedById(string id)
        {

            var historyOfActionsRemove = historyOfActions.FirstOrDefault(t => t.Id.Equals(id));

            if (historyOfActions != null)
            {
                historyOfActions.Remove(historyOfActionsRemove!);

            }

            return historyOfActionsRemove;
        }


        public HistoryOfActions? createHistoryOfActions( string accountId, DateTime actionDate, ActionType actionType, string employeeId, string? bankAccountId)
        {

            Guid guid = Guid.NewGuid();
            string id = guid.ToString();

            var HistoryOfActionsToEdit = historyOfActions.Find(c => c.Id.Equals(id));

            if (HistoryOfActionsToEdit != null)
            {

                throw new Exception($"This history of action Id : {id} , already exist");


            }
            HistoryOfActions newHistoryOfActions = new HistoryOfActions(id, accountId, actionDate, actionType, employeeId, bankAccountId);
            historyOfActions.Add(newHistoryOfActions);

            return newHistoryOfActions;
        }

        public bool EditHistoryOfActions(string id, string accountId, DateTime actionDate, ActionType actionType, string employeeId, string bankAccountId)
        {
            var historyToEdit = historyOfActions.Find(h => h.Id.Equals(id));

            
            if (historyToEdit == null)
            {
                throw new Exception($"No history of action found with Id: {id}");
            }
            historyToEdit.AccountId = accountId;
            historyToEdit.ActionDate = actionDate;
            historyToEdit.ActionType = actionType;
            historyToEdit.EmployeeId = employeeId;
            historyToEdit.BankAccountId = bankAccountId;
            return true;
        }


    }

}