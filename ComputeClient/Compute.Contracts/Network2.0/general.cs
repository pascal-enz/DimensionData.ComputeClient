﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

namespace DD.CBU.Compute.Api.Contracts.Network20
{
	

// 
// This source code was auto-generated by xsd, Version=4.0.30319.33440.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:didata.com:api:cloud:types")]
[System.Xml.Serialization.XmlRootAttribute("response", Namespace = "urn:didata.com:api:cloud:types", IsNullable = false)]
public partial class ResponseType
{

	private string operationField;

	private string responseCodeField;

	private string messageField;

	private NameValuePairType[] infoField;

	private NameValuePairType[] warningField;

	private NameValuePairType[] errorField;

	private string requestIdField;

	/// <remarks/>
	public string operation
	{
		get
		{
			return this.operationField;
		}
		set
		{
			this.operationField = value;
		}
	}

	/// <remarks/>
	public string responseCode
	{
		get
		{
			return this.responseCodeField;
		}
		set
		{
			this.responseCodeField = value;
		}
	}

	/// <remarks/>
	public string message
	{
		get
		{
			return this.messageField;
		}
		set
		{
			this.messageField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("info")]
	public NameValuePairType[] info
	{
		get
		{
			return this.infoField;
		}
		set
		{
			this.infoField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("warning")]
	public NameValuePairType[] warning
	{
		get
		{
			return this.warningField;
		}
		set
		{
			this.warningField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("error")]
	public NameValuePairType[] error
	{
		get
		{
			return this.errorField;
		}
		set
		{
			this.errorField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string requestId
	{
		get
		{
			return this.requestIdField;
		}
		set
		{
			this.requestIdField = value;
		}
	}
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:didata.com:api:cloud:types")]
public partial class NameValuePairType
{

	private string nameField;

	private string valueField;

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string name
	{
		get
		{
			return this.nameField;
		}
		set
		{
			this.nameField = value;
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string value
	{
		get
		{
			return this.valueField;
		}
		set
		{
			this.valueField = value;
		}
	}
}

}
