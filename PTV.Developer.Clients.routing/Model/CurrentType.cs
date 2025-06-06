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
    /// The type of the electric current, direct or alternating. 
    /// </summary>
    /// <value>The type of the electric current, direct or alternating. </value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CurrentType
    {
        /// <summary>
        /// Enum DIRECT for value: DIRECT
        /// </summary>
        [EnumMember(Value = "DIRECT")]
        DIRECT = 1,

        /// <summary>
        /// Enum ALTERNATING for value: ALTERNATING
        /// </summary>
        [EnumMember(Value = "ALTERNATING")]
        ALTERNATING = 2
    }

}
