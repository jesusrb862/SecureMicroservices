﻿using System.Net.Http;
using IdentityModel.Client;
using System;
using System.Threading;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Movies.API.HttpHandlers
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ClientCredentialsTokenRequest _tokenRequest;

        public AuthenticationDelegatingHandler(IHttpClientFactory httpClientFactory,
            ClientCredentialsTokenRequest tokenRequest)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _tokenRequest = tokenRequest ?? throw new ArgumentNullException(nameof(tokenRequest));
        }

        //private readonly IHttpContextAccessor _httpContextAccessor;

        //public AuthenticationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("IDPClient");

            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(_tokenRequest);
            if (tokenResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong when requesting the access token");

            }
            request.SetBearerToken(tokenResponse.AccessToken);

            //var accessToken = await _httpContextAccessor
            //    .HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            //if (!string.IsNullOrWhiteSpace(accessToken))
            //{
            //    request.SetBearerToken(accessToken);
            //}

            return await  base.SendAsync(request, cancellationToken);
        }
    }
}
