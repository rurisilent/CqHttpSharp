using System;
using System.Collections.Generic;
using System.Text;

namespace CqHttpSharp.Message
{
    public enum CqContactType
    {
        qq,
        group
    }

    public enum CqMusicType
    {
        qq,
        cloudmusic,
        xm
    }

    public enum CqImageType
    {
        normal,
        flash,
        show
    }

    public enum CqImageSubType
    {
        normal = 0,
        sticker = 1,
        hot = 2,
        battle = 3,
        intelligence = 4,
        stamped = 7,
        selfie = 8,
        stamped_ads = 9,
        test_1 = 10,
        hotnews = 13
    }

    public enum CqImageEffectType
    {
        normal = 40000,
        phantom = 40001,
        shake = 40002,
        birthday = 40003,
        loveyou = 40004,
        findfriend = 40005
    }

    public enum CqGiftType
    {
        sweek_wink = 0,
        happy_water = 1,
        lucky_band = 2,
        cappuccino = 3,
        cat_watch = 4,
        furry_gloves = 5,
        rainbow_candy = 6,
        be_strong = 7,
        love_microphone = 8,
        hold_your_hands = 9,
        cute_kitty = 10,
        mysterious_mask = 11,
        i_am_so_busy = 12,
        warmhearted_mask = 13
    }
}
