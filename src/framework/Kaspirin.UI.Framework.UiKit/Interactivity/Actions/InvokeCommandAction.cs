// Copyright © 2024 AO Kaspersky Lab.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Kaspirin.UI.Framework.Mvvm;

namespace Kaspirin.UI.Framework.UiKit.Interactivity.Actions
{
    public sealed class InvokeCommandAction : TriggerAction<DependencyObject>
    {
        #region Command

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(InvokeCommandAction),
            new PropertyMetadata(default(ICommand)));

        #endregion

        #region CommandParameter

        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            nameof(CommandParameter),
            typeof(object),
            typeof(InvokeCommandAction),
            new PropertyMetadata(default(object)));

        #endregion

        protected override void Invoke(object parameter)
        {
            if (AssociatedObject == null)
            {
                return;
            }

            if (Command != null)
            {
                InvokeCommand();
            }
            else if (Command == null && TryResolveCommand())
            {
                InvokeCommand();
            }
        }

        private bool TryResolveCommand()
        {
            var commandBindingExpression = BindingOperations.GetBindingExpressionBase(this, CommandProperty);
            if (commandBindingExpression != null)
            {
                if (commandBindingExpression.Status == BindingStatus.Unattached)
                {
                    RestoreUnattachedBinding();

                    return Command != null;
                }
                else
                {
                    _trace.TraceError($"Command property has unexpected binding status '{commandBindingExpression.Status}' " +
                                      $"in {nameof(InvokeCommandAction)} for {AssociatedObject}.");
                }
            }
            else
            {
                _trace.TraceError($"Command property is not set in {nameof(InvokeCommandAction)} for {AssociatedObject}.");
            }

            return false;
        }

        private void RestoreUnattachedBinding()
        {
            var commandBindingExpression = BindingOperations.GetBindingExpressionBase(this, CommandProperty);
            if (commandBindingExpression != null)
            {
                commandBindingExpression = BindingOperations.SetBinding(this, CommandProperty, commandBindingExpression.ParentBindingBase.Clone());
                commandBindingExpression.UpdateTarget();
            }

            var commandParameterBindingExpression = BindingOperations.GetBindingExpressionBase(this, CommandParameterProperty);
            if (commandParameterBindingExpression != null)
            {
                commandParameterBindingExpression = BindingOperations.SetBinding(this, CommandParameterProperty, commandParameterBindingExpression.ParentBindingBase.Clone());
                commandParameterBindingExpression.UpdateTarget();
            }
        }

        private void InvokeCommand()
        {
            Guard.IsNotNull(Command);

            var invoked = CommandHelper.ExecuteCommand(Command, CommandParameter);
            if (invoked)
            {
                _trace.TraceInformation($"Command invoked in {nameof(InvokeCommandAction)} for {AssociatedObject}.");
            }
        }

        private static readonly ComponentTracer _trace = ComponentTracer.Get(UIKitComponentTracers.Interactivity);
    }
}
