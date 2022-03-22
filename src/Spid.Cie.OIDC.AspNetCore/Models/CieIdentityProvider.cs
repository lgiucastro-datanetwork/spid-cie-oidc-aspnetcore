﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Spid.Cie.OIDC.AspNetCore.Models;

internal sealed class CieIdentityProvider : IdentityProvider
{
    internal override IdentityProviderType Type { get => IdentityProviderType.CIE; }

    public override IEnumerable<string> FilterRequestedClaims(ClaimTypes[] requestedClaims)
    {
        List<string> claims = new();
        foreach (var requestedClaim in requestedClaims)
        {
            var mappedClaim = requestedClaim.Value switch
            {
                nameof(ClaimTypes.Name) => CieConst.given_name,
                nameof(ClaimTypes.FamilyName) => CieConst.family_name,
                nameof(ClaimTypes.FiscalNumber) => CieConst.fiscal_number,
                nameof(ClaimTypes.Email) => CieConst.email,
                nameof(ClaimTypes.Address) => CieConst.address,
                nameof(ClaimTypes.DateOfBirth) => CieConst.birthdate,
                nameof(ClaimTypes.Gender) => CieConst.gender,
                nameof(ClaimTypes.PlaceOfBirth) => CieConst.place_of_birth,
                nameof(ClaimTypes.PhoneNumber) => CieConst.phone_number,
                nameof(ClaimTypes.DocumentDetails) => CieConst.document_details,
                nameof(ClaimTypes.EmailVerified) => CieConst.email_verified,
                nameof(ClaimTypes.IdANPR) => CieConst.idANPR,
                nameof(ClaimTypes.EDeliveryService) => CieConst.e_delivery_service,
                nameof(ClaimTypes.PhoneNumberVerified) => CieConst.phone_number_verified,
                nameof(ClaimTypes.PhysicalPhoneNumber) => CieConst.physical_phone_number,
                _ => null,
            };
            if (!string.IsNullOrWhiteSpace(mappedClaim))
                claims.Add(mappedClaim);
        }
        return claims;
    }

    public override string GetAcrValue(SecurityLevel securityLevel)
        => securityLevel switch
        {
            Models.SecurityLevel.L1 => base.SupportedAcrValues.Contains(CieConst.Cie_L1) ? CieConst.Cie_L1 : CieConst.DefaultAcr,
            Models.SecurityLevel.L3 => base.SupportedAcrValues.Contains(CieConst.Cie_L3) ? CieConst.Cie_L3 : CieConst.DefaultAcr,
            _ => base.SupportedAcrValues.Contains(CieConst.Cie_L2) ? CieConst.Cie_L2 : CieConst.DefaultAcr,
        };
}