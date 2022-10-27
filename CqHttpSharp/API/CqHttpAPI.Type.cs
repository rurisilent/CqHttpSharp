using System;
using System.Collections.Generic;
using System.Text;

namespace CqHttpSharp.API
{
    public enum CqHttpAPIMessageType
    {
        normal,
        group
    }

    public enum CqHttpAPIGroupHonorType
    {
        all,
        talkative,
        performer,
        legend,
        strong_newbie,
        emotion
    }

    public enum CqHttpAPIAudioType
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
