namespace FormBot.Dialogs
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

#pragma warning disable 1998

    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private const string TakeVMSnapOption = "Take snap of Virtual Machine";
        private const string AddADAccountOption = "Add Active Directory Account";
        private const string UnlockADAccountOption = "Unlock Active Directory Account";
        private const string AddVMOption = "Add Virtual Machine";

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            PromptDialog.Choice(
                context,
                this.AfterChoiceSelected,
                new[] { AddADAccountOption, UnlockADAccountOption, AddVMOption, TakeVMSnapOption },
                "Hi I am Maggie Your Virtual IT Assistant,What do you want to do today?",
                "I am sorry but I didn't understand that. I need you to select one of the options below",
                attempts: 2);
        }

        private async Task AfterChoiceSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                var selection = await result;

                switch (selection)
                {
                    case UnlockADAccountOption:
                        context.Call(new UnlockADAccountDialog(), this.AfterUnlockADAccount);
                        break;
                    case AddADAccountOption:
                        context.Call(new AddADAccountDialog(), this.AfterAddADAccount);
                        break;
                    case TakeVMSnapOption:
                        context.Call(new TakeVMSnapDialog(), this.AfterTakeVMSnap);
                        break;
                    case AddVMOption:
                        context.Call(new AddVMDialog(), this.AfterAddVM);
                        break;


                        //case TakeVMSnap:
                        //  context.Call(new ResetPasswordDialog(), this.AfterResetPassword);
                        //break;
                }
            }
            catch (TooManyAttemptsException)
            {
                await this.StartAsync(context);
            }
        }

        private Task AfterAddVM(IDialogContext context, IAwaitable<object> result)
        {
            throw new NotImplementedException();
        }

        private Task AfterUnlockADAccount(IDialogContext context, IAwaitable<object> result)
        {
            throw new NotImplementedException();
        }

        private Task AfterTakeVMSnap(IDialogContext context, IAwaitable<object> result)
        {
            throw new NotImplementedException();
        }

        private Task AfterAddADAccount(IDialogContext context, IAwaitable<object> result)
        {
            throw new NotImplementedException();
        }

    }

}