/*
 * Routing
 *
 * With the Routing service you can calculate routes from A to B taking into account vehicle-specific restrictions, traffic situations, toll, emissions, drivers' working hours, service times and opening intervals.
 *
 * The version of the OpenAPI document: 1.35
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = PTV.Developer.Clients.routing.Client.OpenAPIDateConverter;

namespace PTV.Developer.Clients.routing.Model
{
    /// <summary>
    /// Indicates if a route section is entered or exited. Not present for violation events of type _SCHEDULE_.  * &#x60;ENTER&#x60; - Entering a section.    * &#x60;EXIT&#x60; - Exiting a section.    * &#x60;PASS&#x60; - Passing an intersection, a gate or a specific location. Can only occur with violation events of type _RESTRICTED_ACCESS_ and _PROHIBITED_BY_INTERSECTING_POLYLINE_.
    /// </summary>
    /// <value>Indicates if a route section is entered or exited. Not present for violation events of type _SCHEDULE_.  * &#x60;ENTER&#x60; - Entering a section.    * &#x60;EXIT&#x60; - Exiting a section.    * &#x60;PASS&#x60; - Passing an intersection, a gate or a specific location. Can only occur with violation events of type _RESTRICTED_ACCESS_ and _PROHIBITED_BY_INTERSECTING_POLYLINE_.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AccessType
    {
        /// <summary>
        /// Enum ENTER for value: ENTER
        /// </summary>
        [EnumMember(Value = "ENTER")]
        ENTER = 1,

        /// <summary>
        /// Enum EXIT for value: EXIT
        /// </summary>
        [EnumMember(Value = "EXIT")]
        EXIT = 2,

        /// <summary>
        /// Enum PASS for value: PASS
        /// </summary>
        [EnumMember(Value = "PASS")]
        PASS = 3
    }

}
