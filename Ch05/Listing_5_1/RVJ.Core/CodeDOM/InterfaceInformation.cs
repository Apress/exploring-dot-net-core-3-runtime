#region Namespaces
using System;
using System.Collections.Generic;
#endregion

namespace RVJ.Core.CodeDOM {

    public class InterfaceInformation {

        #region Private fields
        private readonly String _name;
        private readonly List<MethodInformation> _methods;
        private readonly List<PropertyInformation> _properties;
        #endregion

        #region Constructors
        private InterfaceInformation() { }

        public InterfaceInformation( String name ) : base() {

            if ( !String.IsNullOrEmpty( name ) && !String.IsNullOrWhiteSpace( name ) ) this._name = name;

        }
        #endregion

        #region Properties
        public List<PropertyInformation> Properties {

            get {

                return this._properties;

            }

        }

        public List<MethodInformation> Methods {

            get {

                return this._methods;

            }

        }
        #endregion

    }


};