﻿namespace FormBot.Dialogs
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using Newtonsoft.Json;

#pragma warning disable 1998

    [Serializable]
    public class RootDialog : IDialog<object>
    {
        
        private const string TakeVMSnapOption = "Take snap of Virtual Machine";
        private const string AddADAccountOption = "Add Active Directory Account";
        private const string UnlockADAccountOption = "Unlock Active Directory Account";
        private const string AddVMOption = "Add Virtual Machine";
        string SenderId;
        string SenderName;


        public async Task StartAsync(IDialogContext context)
        {
            string data = context.Activity.ToString();

            SenderId = context.Activity.From.Id;
            SenderName = context.Activity.From.Name;



            //System.IO.File.AppendAllText("ErrorLog.txt", "\n" + System.DateTime.Now + result);

            //var FromDetails = JsonConvert.DeserializeObject<FromDetails>(result);
            //await context.PostAsync($"Thanks {SenderName} : {SenderId}");
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

        private async Task AfterAddVM(IDialogContext context, IAwaitable<object> result)
        {
            var success = await result;
            await this.StartAsync(context);
        }

        private async Task AfterUnlockADAccount(IDialogContext context, IAwaitable<object> result)
        {
            var success = await result;
            await this.StartAsync(context);
        }

        private async Task AfterTakeVMSnap(IDialogContext context, IAwaitable<object> result)
        {
            var success = await result;
            await this.StartAsync(context);
        }

        private async Task AfterAddADAccount(IDialogContext context, IAwaitable<object> result)
        {
            var success = await result;
            await this.StartAsync(context);
        }

    }

}