using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;

namespace WiiTrakClient.Features.Drivers.Components
{
    public partial class UpdateCartDialog: ComponentBase
    {
        [Parameter]
        public CartDto? Cart { get; set; }

        [Parameter]
        public List<RepairIssueDto>? RepairIssues { get; set; }

        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        int _selectedStatusInt = 0;
        int _seletedConditionInt = 0;
        string _seletedIssue = "";
        string _otherText = "";

        protected override void OnParametersSet()
        {
            if (Cart is not null)
            {
                _selectedStatusInt = (int)Cart.Status;
                _seletedConditionInt = (int)Cart.Condition;
            }

            if (RepairIssues is not null)
            {
                _seletedIssue = RepairIssues[0].Issue;

                if (!RepairIssues.Any(x => x.Issue.Equals("Other")))
                {
                    RepairIssues.Add(new RepairIssueDto
                    {
                        Issue = "Other"
                    });
                }
            }
        }

        void Submit()
        {
            if (_selectedStatusInt > -1)
            {
                Cart.Status = (AssetStatus)_selectedStatusInt;
            }
            if (_seletedConditionInt > -1)
            {
                Cart.Condition = (AssetCondition)_seletedConditionInt;
            }

            MudDialog.Close(DialogResult.Ok(true));
        }
        void Cancel() => MudDialog.Cancel();

        void StatusChangedHandler(int statusInt)
        {
            _selectedStatusInt = statusInt;
        }

        void ConditionChangedHandler(int conditionInt)
        {
            _seletedConditionInt = conditionInt;
        }
    }
}