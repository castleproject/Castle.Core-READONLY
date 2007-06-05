// Copyright 2004-2007 Castle Project - http://www.castleproject.org/
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

namespace Castle.Components.Validator
{
	using System;
	using System.Collections;

	/// <summary>
	/// This is a meta validator. 
	/// It is only useful to test a source content before setting it on the 
	/// target instance.
	/// </summary>
	public class IntegerValidator : AbstractValidator
	{
		/// <summary>
		/// Checks if the <c>fieldValue</c> can be converted to a valid Integer.
		/// Null or empty value NOT allowed.
		/// </summary>
		/// <param name="instance">The target type instance</param>
		/// <param name="fieldValue">The property/field value. It can be null.</param>
		/// <returns>
		/// 	<c>true</c> if the value is accepted (has passed the validation test)
		/// </returns>
		public override bool IsValid(object instance, object fieldValue)
		{
			if (fieldValue == null) return false;

			string stringValue = fieldValue.ToString();

			if (Property.PropertyType == typeof(Nullable<Int16>))
			{
				Int16 intValue;
				return Int16.TryParse(stringValue, out intValue);
			}
			else if (Property.PropertyType == typeof(Nullable<Int64>))
			{
				Int64 intValue;
				return Int64.TryParse(stringValue, out intValue);
			}
			else
			{
				Int32 intValue;
				return Int32.TryParse(stringValue, out intValue);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this validator supports web validation.
		/// </summary>
		/// <value>
		/// 	<see langword="true"/> if web validation is supported; otherwise, <see langword="false"/>.
		/// </value>
		public override bool SupportsWebValidation
		{
			get { return true; }
		}

		/// <summary>
		/// Applies the web validation by setting up one or
		/// more input rules on <see cref="IWebValidationGenerator"/>.
		/// </summary>
		/// <param name="config">The config.</param>
		/// <param name="inputType">Type of the input.</param>
		/// <param name="generator">The generator.</param>
		/// <param name="attributes">The attributes.</param>
		/// <param name="target">The target.</param>
		public override void ApplyWebValidation(WebValidationConfiguration config, InputElementType inputType,
		                                        IWebValidationGenerator generator, IDictionary attributes, string target)
		{
			base.ApplyWebValidation(config, inputType, generator, attributes, target);

			generator.SetAsRequired(target, null);
			generator.SetDigitsOnly(target, BuildErrorMessage());
		}

		/// <summary>
		/// Returns the key used to internationalize error messages
		/// </summary>
		/// <value></value>
		protected override string MessageKey
		{
			get { return MessageConstants.InvalidIntegerMessage; }
		}
	}
}
