using System;
using System.Collections.Generic;
using System.Text;

namespace NapCatSharpLib.API
{
    public enum NapCatAPIMessageType
    {
        normal,
        group
    }

    public enum NapCatAPIGroupHonorType
    {
        all,
        talkative,
        performer,
        legend,
        strong_newbie,
        emotion
    }

    public enum NapCatAPIAudioType
    {
        mp3,
        amr,
        wma,
        m4a,
        spx,
        ogg,
        wav,
        flac
    }
}
