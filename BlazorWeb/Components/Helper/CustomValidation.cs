using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.Respons;

namespace Web.Components.Helper
{
    public class CustomValidation : ComponentBase
    {
        [CascadingParameter]
        private EditContext? _editContext { get; set; }
        private  ValidationMessageStore? _messageStore;

        protected override void OnInitialized()
        {
            if (_editContext != null)
            {
                _messageStore = new ValidationMessageStore(_editContext);
                _editContext.OnValidationRequested += (s, e) => _messageStore.Clear();
                _editContext.OnFieldChanged += (s, e) => _messageStore.Clear();
            }
        }

        public void AddError(string propertyName, string message)
        {
            var fieldIdentifier = new FieldIdentifier(_editContext!.Model,propertyName);

            _messageStore!.Add(fieldIdentifier, message);
        }

        public void AddErrors(IEnumerable<FailureError> errors)
        {
            Clear();

            foreach (var error in errors)
            {
                AddError(error.Code!, error.Message!);
            }

            _editContext!.NotifyValidationStateChanged();
        }

        public void DisplayErrors(string[] errors)
        {
            foreach (var error in errors)
            {
                _messageStore!.Add(_editContext!.Field(""), error);
            }

            _editContext!.NotifyValidationStateChanged();
        }
        public void Clear()
        {
            _messageStore!.Clear();
            _editContext!.NotifyValidationStateChanged();
        }
    }
}