// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using WalletService.IdentityServer.Constants;

namespace WalletService.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource(ResourcesNames.WalletService){
                Scopes = { Scopes.WalletService }
            },
            new ApiResource(ResourcesNames.TransactionService){
                Scopes = { Scopes.TransactionService }
            },
            new ApiResource(ResourcesNames.GateWay){
                Scopes = { Scopes.GateWay }
            },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };
        public static IEnumerable<IdentityResource> IdentityResources =>
              new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile()
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
             new ApiScope[]
            {
                new ApiScope(Scopes.WalletService),
                new ApiScope(Scopes.TransactionService),
                new ApiScope(Scopes.GateWay),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                 new Client
                {
                    ClientName="WalletService.Client",
                    ClientId="webClientUser",
                    AllowOfflineAccess = true,
                    ClientSecrets={new Secret("mySecret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={
                        Scopes.WalletService,
                        Scopes.TransactionService,
                        Scopes.GateWay,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    },
                    AccessTokenLifetime=1*60*60,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime=(int) (DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RefreshTokenUsage=TokenUsage.ReUse
                }
            };
    }
}