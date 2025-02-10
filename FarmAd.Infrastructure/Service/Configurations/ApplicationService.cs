using FarmAd.Application.Abstractions.Services.Configurations;
using FarmAd.Application.CustomAttributes;
using FarmAd.Application.DTOs.Configuration;
using FarmAd.Application.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace FarmAd.Infrastructure.Service
{
    public class ApplicationService : IApplicationService
    {
        public ApplicationService()
        {

        }
        public List<Menu> GetAuthorizeDefinitionEndpoint(Type type)
        {
            List<Menu> menus = new();
            Assembly assembly = Assembly.GetAssembly(type);
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));
            if (controllers != null)
            {
                foreach (var controller in controllers)
                {
                    var actions = controller.GetMethods().Where(x => x.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                    if (actions != null)
                    {
                        foreach (var action in actions)
                        {

                            var attributes = action.GetCustomAttributes(true);
                            if (attributes != null)
                            {
                                Menu menu = null;
                                var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                                if (!menus.Any(x => x.Name == authorizeDefinitionAttribute.Menu))

                                {
                                    menu = new() { Name = authorizeDefinitionAttribute.Menu };
                                    menus.Add(menu);
                                }
                                else
                                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);
                                Application.DTOs.Configuration.Action _action = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionType), authorizeDefinitionAttribute.ActionType),
                                    Definition = authorizeDefinitionAttribute.Definition,


                                };
                                var htttAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                if (htttAttribute != null)
                                    _action.HttpType = htttAttribute.HttpMethods.First();
                                else
                                    _action.HttpType = HttpMethods.Get;
                                _action.Code = $"{controller.Name.Substring(0, controller.Name.Length - 10)}.{_action.HttpType}.{_action.ActionType}";
                                menu.Actions.Add(_action);
                            }
                        }

                    }
                }
            }
            return menus;
        }
    }
}
