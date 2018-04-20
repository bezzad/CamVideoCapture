using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;

namespace VideoProcessing
{
    public static class ImageFilters
    {
        private static EuclideanColorFiltering eulideanColorFilter = new EuclideanColorFiltering();
        private static GrayscaleBT709 grayscaleFilter = new GrayscaleBT709();

        /// <summary>
        /// List of most common display resolutions in the first half of 2014
        /// </summary>
        public static List<DisplayResulation> DisplayResulationStandards = DisplayStandards();
        public static BlobCounter BlobCounterFilter = new BlobCounter() { FilterBlobs = true, MinHeight = 20, MinWidth = 20, ObjectsOrder = ObjectsOrder.Size };
        public static int Range = 110;
        public static Color FilterColor = Color.Black;

        private static List<DisplayResulation> DisplayStandards()
        {
            ///
            /// Return list of most common display resolutions in the first half of 2014
            ///
            return new List<DisplayResulation>()
            {
                #region Assigned Display Resulation Standards
                // Reference:
                // http://en.wikipedia.org/wiki/List_of_common_resolutions
                //
                new DisplayResulation(@"Microvision",new Size(16,16), new AspectRatio("1:1"), new AspectRatio("1:1"), new AspectRatio("1:1"), 256),
                new DisplayResulation(@"Timex Datalink USB",new Size(42,11), new AspectRatio("42:11"), new AspectRatio("1:1"), new AspectRatio("5:9"), 462),
                new DisplayResulation(@"PocketStation",new Size(32,32), new AspectRatio("1:1"), new AspectRatio("1:1"), new AspectRatio("1:1"), 1024),
                new DisplayResulation(@"Etch A Sketch Animator",new Size(40,30), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 1200),
                new DisplayResulation(@"Epson RC-20",new Size(42,32), new AspectRatio("21:16"), new AspectRatio("1:1"), new AspectRatio("0.762"), 1344),
                new DisplayResulation(@"GameKing I (GM-218), VMU",new Size(48,32), new AspectRatio("3:2"), new AspectRatio("3:2"), new AspectRatio("1:1"), 1536),
                new DisplayResulation(@"Etch A Sketch Animator 2000",new Size(60,40), new AspectRatio("3:2"), new AspectRatio("3:2"), new AspectRatio("1:1"), 2400),
                new DisplayResulation(@"Nokia 3210, and many other early Nokia Phones",new Size(84,48), new AspectRatio("7:4"), new AspectRatio("2:1"), new AspectRatio("1.143"), 4032),
                new DisplayResulation(@"Hartung Game Master",new Size(64,64), new AspectRatio("1:1"), new AspectRatio("1:1"), new AspectRatio("1:1"), 4096),
                new DisplayResulation(@"Field Technology CxMP Ltd. Smart Watch",new Size(72,64), new AspectRatio("9:8"), new AspectRatio("1:1"), new AspectRatio("0.889"), 4608),
                new DisplayResulation(@"Epoch Game Pocket Computer",new Size(75,64), new AspectRatio("75:64"), new AspectRatio("1:1"), new AspectRatio("1:1.171875"), 4800),
                new DisplayResulation(@"Entex Adventure Vision",new Size(150,40), new AspectRatio("15:4"), new AspectRatio("3.75"), new AspectRatio("1:1"), 6000),
                new DisplayResulation(@"First Graphing calculators: Casio fx-7000G, TI-81",new Size(96,64), new AspectRatio("3:2"), new AspectRatio("3:2"), new AspectRatio("1:1"), 6144),
                new DisplayResulation(@"Pokémon mini",new Size(96,64), new AspectRatio("3:2"), new AspectRatio("3:2"), new AspectRatio("1:1"), 6144),
                new DisplayResulation(@"TRS-80",new Size(128,48), new AspectRatio("8:3"), new AspectRatio("3:2"), new AspectRatio("0.563"), 6144),
                new DisplayResulation(@"Nokia Series 40 phones",new Size(96,65), new AspectRatio("96:65"), new AspectRatio("3:2"), new AspectRatio("1.016"), 6240),
                new DisplayResulation(@"Ruputer",new Size(102,64), new AspectRatio("51:32"), new AspectRatio("8:5"), new AspectRatio("1.004"), 6528),
                new DisplayResulation(@"MetaWatch Strata & Frame watches",new Size(96,96), new AspectRatio("1:1"), new AspectRatio("1:1"), new AspectRatio("1:1"), 9216),
                new DisplayResulation(@"PixelVision",new Size(120,90), null, new AspectRatio("4:3"), null, 10800),
                new DisplayResulation(@"SQCIF (Sub Quarter CIF)",new Size(128,96), new AspectRatio("1.33:1"), null, null, 12288),
                new DisplayResulation(@"Atari Portfolio, TRS-80 Model 100",new Size(240,64), new AspectRatio("15:4"), new AspectRatio("3.75"), new AspectRatio("1:1"), 15360),
                new DisplayResulation(@"Atari Lynx",new Size(160,102), new AspectRatio("80:51"), new AspectRatio("8:5"), new AspectRatio("1.02"), 16320),
                new DisplayResulation(@"Sony SmartWatch, Sifteo cubes",new Size(128,128), new AspectRatio("1:1"), new AspectRatio("1:1"), new AspectRatio("1:1"), 16384),
                new DisplayResulation(@"(QQVGA)",new Size(160,120), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 19200),
                new DisplayResulation(@"Nintendo Game Boy, Game Boy Color (GB), Sega Game Gear (GG)",new Size(160,144), new AspectRatio("10:9"), new AspectRatio("10:9"), new AspectRatio("1:1"), 23040),
                new DisplayResulation(@"Pebble E-Paper Watch",new Size(144,168), new AspectRatio("6:7"), new AspectRatio("6:7"), new AspectRatio("1:1"), 24192),
                new DisplayResulation(@"Neo Geo Pocket Color",new Size(160,152), new AspectRatio("20:19"), new AspectRatio("20:19"), new AspectRatio("1:1"), 24320),
                new DisplayResulation(@"QCIF (Quarter CIF)",new Size(176,144), new AspectRatio("1.22:1"), null, null, 25344),
                new DisplayResulation(@"Palm LoRES",new Size(160,160), new AspectRatio("1:1"), new AspectRatio("1:1"), new AspectRatio("1:1"), 25600),
                new DisplayResulation(@"Apple II HiRes (6 color) and Apple IIe Double HiRes (16 color), grouping subpixels",new Size(140,192), new AspectRatio("35:48"), new AspectRatio("4:3"), new AspectRatio("1.828"), 26880),
                new DisplayResulation(@"VIC-II multicolor, IBM PCjr 16-color, Amstrad CPC",new Size(160,200), new AspectRatio("4:5"), new AspectRatio("4:3"), new AspectRatio("5:3"), 32000),
                new DisplayResulation(@"WonderSwan",new Size(224,144), new AspectRatio("14:9"), new AspectRatio("14:9"), new AspectRatio("1:1"), 32256),
                new DisplayResulation(@"Nokia Series 60 smartphones (Nokia 7650, plus First and Second Edition models only)",new Size(208,176), new AspectRatio("13:11"), new AspectRatio("13:11"), new AspectRatio("1:1"), 36608),
                new DisplayResulation(@"Nintendo Game Boy Advance (HQVGA)",new Size(240,160), new AspectRatio("3:2"), new AspectRatio("3:2"), new AspectRatio("1:1"), 38400),
                new DisplayResulation(@"Older Java MIDP devices like Sony Ericsson K600",new Size(220,176), new AspectRatio("5:4"), new AspectRatio("5:4"), new AspectRatio("1:1"), 38720),
                new DisplayResulation(@"Acorn BBC 20 column modes",new Size(160,256), new AspectRatio("5:8"), new AspectRatio("4:3"), new AspectRatio("2.133"), 40960),
                new DisplayResulation(@"Nokia 5500 Sport",new Size(208,208), new AspectRatio("1:1"), new AspectRatio("1:1"), new AspectRatio("1:1"), 43264),
                new DisplayResulation(@"TMS9918 Modes 1 (e.g. TI-99/4a) and 2, ZX Spectrum, MSX, Nintendo DS (each screen)",new Size(256,192), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 49152),
                new DisplayResulation(@"Apple II HiRes (1 bit per pixel)",new Size(280,192), new AspectRatio("35:24"), new AspectRatio("4:3"), new AspectRatio("0.914"), 53760),
                new DisplayResulation(@"Samsung Gear Fit",new Size(432,128), new AspectRatio("27:8"), new AspectRatio("1:1"), new AspectRatio("0.296"), 55296),
                new DisplayResulation(@"Apple iPod Nano 6G",new Size(240,240), new AspectRatio("1:1"), new AspectRatio("1:1"), new AspectRatio("1:1"), 57600),
                new DisplayResulation(@"Atari 400/800",new Size(320,192), new AspectRatio("5:3"), new AspectRatio("5:3"), new AspectRatio("1:1"), 61440),
                new DisplayResulation(@"CGA 4-color, Atari ST 16 color, Commodore 64 VIC-II Hires, Amiga OCS NTSC Lowres, Apple IIGSLoRes, MCGA",new Size(320,200), new AspectRatio("8:5"), new AspectRatio("4:3"), new AspectRatio("0.833"), 64000),
                new DisplayResulation(@"Elektronika BK",new Size(256,256), new AspectRatio("1:1"), new AspectRatio("1:1"), new AspectRatio("1:1"), 65536),
                new DisplayResulation(@"UIQ 2.x based smartphones",new Size(320,208), new AspectRatio("20:13"), new AspectRatio("3:2"), new AspectRatio("0.975"), 66560),
                new DisplayResulation(@"Sega Nomad, Neo Geo AES",new Size(320,224), new AspectRatio("10:7"), new AspectRatio("3:2"), new AspectRatio("1.05"), 71680),
                new DisplayResulation(@"QVGA, Mega Drive, Nintendo 3DS (lower screen)",new Size(320,240), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 76800),
                new DisplayResulation(@"Acorn BBC 40 column modes, Amiga OCS PAL Lowres",new Size(320,256), new AspectRatio("5:4"), new AspectRatio("5:4"), new AspectRatio("1:1"), 81920),
                new DisplayResulation(@"Video CD (NTSC)",new Size(352,240), null, new AspectRatio("4:3"), null, 84480),
                new DisplayResulation(@"Apple iPod Nano 5G",new Size(376,240), new AspectRatio("47:30"), new AspectRatio("14:9"), new AspectRatio("0.993"), 90240),
                new DisplayResulation(@"WQVGA (common on Windows Mobile 6 handsets)",new Size(400,240), new AspectRatio("5:3"), new AspectRatio("5:3"), new AspectRatio("1:1"), 96000),
                new DisplayResulation(@"Video CD (PAL)",new Size(352,288), null, null, null, 101376),
                new DisplayResulation(@"CIF (or FCIF)",new Size(352,288), new AspectRatio("1.22:1"), null, null, 101376),
                new DisplayResulation(@"Palm (PDA) HiRES, Samsung Galaxy Gear",new Size(320,320), new AspectRatio("1:1"), new AspectRatio("1:1"), new AspectRatio("1:1"), 102400),
                new DisplayResulation(@"WQVGA, Apple iPod Nano 7G",new Size(432,240), new AspectRatio("9:5"), new AspectRatio("9:5"), new AspectRatio("1:1"), 103680),
                new DisplayResulation(@"Apple IIe Double Hires (1 bit per pixel)",new Size(560,192), new AspectRatio("35:12"), new AspectRatio("4:3"), new AspectRatio("0.457"), 107520),
                new DisplayResulation(@"TurboExpress",new Size(400,270), new AspectRatio("40:27"), new AspectRatio("3:2"), new AspectRatio("1.013"), 108000),
                new DisplayResulation(@"A WQVGA variant, used commonly for Portable DVD Players, Digital photo frames, GPS receivers and devices such as the Kenwood DNX-5120, and Glospace SGK-70. Often falsely marketed as 16:9",new Size(480,234), new AspectRatio("80:39"), new AspectRatio("16:9"), new AspectRatio("0.866"), 112320),
                new DisplayResulation(@"Quarter SVGA (selectable in some PC shooters)",new Size(400,300), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 120000),
                new DisplayResulation(@"Teletext and Viewdata 40×25 character screens (PAL non-interlaced)",new Size(480,250), new AspectRatio("48:25"), new AspectRatio("4:3"), new AspectRatio("0.694"), 120000),
                new DisplayResulation(@"Atari ST 4 color, CGA mono, Amiga OCS NTSC Hires, Apple IIGS HiRes, Nokia Series 80 smartphones",new Size(640,200), new AspectRatio("16:5"), new AspectRatio("4:3"), new AspectRatio("0.417"), 128000),
                new DisplayResulation(@"Sony PlayStation Portable, Zune HD, Neo Geo X",new Size(480,272), new AspectRatio("30:17"), new AspectRatio("16:9"), new AspectRatio("1.007"), 130560),
                new DisplayResulation(@"UMD",new Size(480,272), null, new AspectRatio("~16:9"), null, 130560),
                new DisplayResulation(@"Elektronika BK, Polyplay",new Size(512,256), new AspectRatio("2:1"), new AspectRatio("2:1"), new AspectRatio("1:1"), 131072),
                new DisplayResulation(@"Nokia Series 60 smartphones (E60, E70, N80, N90)",new Size(416,352), new AspectRatio("13:11"), new AspectRatio("13:11"), new AspectRatio("1:1"), 146432),
                new DisplayResulation(@"HVGA, Palm Tungsten T3 Apple iPhone, Palm (PDA) HiRES+",new Size(480,320), new AspectRatio("3:2"), new AspectRatio("3:2"), new AspectRatio("1:1"), 153600),
                new DisplayResulation(@"HVGA, Handheld PC",new Size(640,240), new AspectRatio("8:3"), new AspectRatio("8:3"), new AspectRatio("1:1"), 153600),
                new DisplayResulation(@"Acorn BBC 80 column modes, Amiga OCS PAL Hires",new Size(640,256), new AspectRatio("5:2"), new AspectRatio("4:3"), new AspectRatio("0.533"), 163840),
                new DisplayResulation(@"China Video Disc (NTSC)",new Size(352,480), null, new AspectRatio("4:3 or 16:9"), null, 168960),
                new DisplayResulation(@"Black & white Macintosh",new Size(512,342), new AspectRatio("256:171"), new AspectRatio("3:2"), new AspectRatio("1.002"), 175104),
                new DisplayResulation(@"Nintendo 3DS (upper screen in 3D mode) (2× 400×240, one for each eye)",new Size(800,240), new AspectRatio("10:3"), new AspectRatio("5:3"), new AspectRatio("0.5"), 192000),
                new DisplayResulation(@"Macintosh LC (12 inch) / Color Classic (also selectable in many PC shooters)",new Size(512,384), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 196608),
                new DisplayResulation(@"China Video Disc (PAL)",new Size(352,576), null, null, null, 202725),
                new DisplayResulation(@"Nokia Series 90 smartphones (7700, 7710)",new Size(640,320), new AspectRatio("2:1"), new AspectRatio("2:1"), new AspectRatio("1:1"), 204800),
                new DisplayResulation(@"EGA",new Size(640,350), new AspectRatio("64:35"), new AspectRatio("4:3"), new AspectRatio("0.729"), 224000),
                new DisplayResulation(@"nHD, used by Nokia 5800, Nokia 5530, Nokia X6, Nokia N97, Nokia N8",new Size(640,360), new AspectRatio("16:9"), new AspectRatio("16:9"), new AspectRatio("1:1"), 230400),
                new DisplayResulation(@"SVCD (NTSC)",new Size(480,480), null, new AspectRatio("4:3 or 16:9"), null, 230400),
                new DisplayResulation(@"Teletext and Viewdata 40×25 character screens (PAL interlaced)",new Size(480,500), new AspectRatio("24:25"), new AspectRatio("4:3"), new AspectRatio("1.389"), 240000),
                new DisplayResulation(@"HGC",new Size(720,348), new AspectRatio("60:29"), new AspectRatio("4:3"), new AspectRatio("0.644"), 250560),
                new DisplayResulation(@"MDA",new Size(720,350), new AspectRatio("72:35"), new AspectRatio("4:3"), new AspectRatio("0.648"), 252000),
                new DisplayResulation(@"Atari ST mono, Amiga OCS NTSC Hires interlaced",new Size(640,400), new AspectRatio("8:5"), new AspectRatio("4:3"), new AspectRatio("0.833"), 256000),
                new DisplayResulation(@"Apple Lisa",new Size(720,364), new AspectRatio("180:91"), new AspectRatio("4:3"), new AspectRatio("0.674"), 262080),
                new DisplayResulation(@"SVCD (PAL)",new Size(480,576), null, null, null, 276480),
                new DisplayResulation(@"SDTV 576i, EDTV 576p",new Size(480,576), null, new AspectRatio("4:3 or 16:9"), null, 276480),
                new DisplayResulation(@"Nokia E90 Communicator",new Size(800,352), new AspectRatio("25:11"), new AspectRatio("25:11"), new AspectRatio("1:1"), 281600),
                new DisplayResulation(@"",new Size(600,480), new AspectRatio("5:4"), new AspectRatio("5:4"), new AspectRatio("1:1"), 288000),
                new DisplayResulation(@"VGA, MCGA (in monochome), Sun-1 color",new Size(640,480), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 307200),
                new DisplayResulation(@"SDTV 480i, EDTV 480p, SMPTE 293M",new Size(640,480), null, new AspectRatio("4:3 or 16:9 or 3:2"), null, 307200),
                new DisplayResulation(@"SDTV 576i, EDTV 576p",new Size(544,576), null, null, null, 313344),
                new DisplayResulation(@"Amiga OCS PAL Hires interlaced",new Size(640,512), new AspectRatio("5:4"), new AspectRatio("4:3"), new AspectRatio("1.066"), 327680),
                new DisplayResulation(@"SDTV 480i, EDTV 480p, SMPTE 293M",new Size(704,480), null, null, null, 337920),
                new DisplayResulation(@"SDTV 480i, EDTV 480p, SMPTE 293M",new Size(720,480), null, null, null, 345600),
                new DisplayResulation(@"DVD (NTSC)",new Size(720,480), null, new AspectRatio("4:3 or 16:9"), null, 345600),
                new DisplayResulation(@"DV NTSC",new Size(720,480), new AspectRatio("3:2"), new AspectRatio("4:3"), new AspectRatio("10:11[citation needed]"), 345600),
                new DisplayResulation(@"D1 NTSC",new Size(720,486), new AspectRatio("40:27"), new AspectRatio("4:3"), new AspectRatio("9:10"), 349920),
                new DisplayResulation(@"Wide VGA (WVGA)",new Size(768,480), new AspectRatio("8:5"), new AspectRatio("8:5"), new AspectRatio("1:1"), 368640),
                new DisplayResulation(@"Wide VGA, List of mobile phones with WVGA display (WGA)",new Size(800,480), new AspectRatio("5:3"), new AspectRatio("5:3"), new AspectRatio("1:1"), 384000),
                new DisplayResulation(@"SDTV 576i, EDTV 576p",new Size(704,576), null, null, null, 405504),
                new DisplayResulation(@"4CIF (4 * CIF)",new Size(704,576), new AspectRatio("1.22:1"), null, null, 405504),
                new DisplayResulation(@"Wide PAL,",new Size(848,480), new AspectRatio("53:30"), new AspectRatio("16:9"), new AspectRatio("1.006"), 407040),
                new DisplayResulation(@"SDTV 480i, EDTV 480p, SMPTE 293M",new Size(852,480), null, null, null, 408960),
                new DisplayResulation(@"List of mobile phones with FWVGA display (FWVGA)",new Size(854,480), new AspectRatio("427:240"), new AspectRatio("16:9"), new AspectRatio("0.999"), 409920),
                new DisplayResulation(@"SDTV 576i, EDTV 576p",new Size(720,576), null, null, null, 414720),
                new DisplayResulation(@"DVD (PAL)",new Size(720,576), null, null, null, 414720),
                new DisplayResulation(@"DV PAL",new Size(720,576), new AspectRatio("5:4"), new AspectRatio("4:3"), new AspectRatio("12:11[citation needed]"), 414720),
                new DisplayResulation(@"D1 PAL",new Size(720,576), new AspectRatio("5:4"), new AspectRatio("4:3"), new AspectRatio("16:15"), 414720),
                new DisplayResulation(@"SDTV 576i, EDTV 576p",new Size(768,576), null, null, null, 442368),
                new DisplayResulation(@"Super VGA (SVGA)",new Size(800,600), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 480000),
                new DisplayResulation(@"Quarter FHD, (AACS ICT), HRHD, Motorola Atrix 4G, Sony XEL-1 (qHD)",new Size(960,540), new AspectRatio("16:9"), new AspectRatio("16:9"), new AspectRatio("1:1"), 518400),
                new DisplayResulation(@"Apple Macintosh Half Megapixel",new Size(832,624), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 519168),
                new DisplayResulation(@"PlayStation Vita",new Size(960,544), new AspectRatio("30:17"), new AspectRatio("16:9"), new AspectRatio("1.007"), 522240),
                new DisplayResulation(@"(PAL 16:9)",new Size(1024,576), new AspectRatio("16:9"), new AspectRatio("16:9"), new AspectRatio("1:1"), 589824),
                new DisplayResulation(@"Apple iPhone 4S, 4th Generation iPod Touch (DVGA)",new Size(960,640), new AspectRatio("3:2"), new AspectRatio("3:2"), new AspectRatio("1:1"), 614400),
                new DisplayResulation(@"WSVGA",new Size(1024,600), new AspectRatio("128:75"), new AspectRatio("16:9"), new AspectRatio("1.041"), 614400),
                new DisplayResulation(@"(Normal laptops)",new Size(1024,640), new AspectRatio("8:5"), new AspectRatio("8:5"), new AspectRatio("1:1"), 655360),
                new DisplayResulation(@"Panasonic DVCPRO100 for 50/60Hz over 720p - SMPTE Resolution",new Size(960,720), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 691200),
                new DisplayResulation(@"Panasonic DVCPRO HD 720p",new Size(960,720), new AspectRatio("4:3"), new AspectRatio("16:9"), new AspectRatio("4:3"), 691200),
                new DisplayResulation(@"Apple iPhone 5 Retina display",new Size(1136,640), new AspectRatio("71:40"), new AspectRatio("16:9"), new AspectRatio("1:1"), 727040),
                new DisplayResulation(@"Common on 14 and 15 inch TFT's and the Apple iPad (XGA)",new Size(1024,768), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 786432),
                new DisplayResulation(@"Sun-1 monochrome",new Size(1024,800), new AspectRatio("32:25"), new AspectRatio("4:3"), new AspectRatio("1.041"), 819200),
                new DisplayResulation(@"Apple PowerBook G4 (original Titanium version)",new Size(1152,768), new AspectRatio("3:2"), new AspectRatio("3:2"), new AspectRatio("1:1"), 884736),
                new DisplayResulation(@"720p (WXGA-H, min.)",new Size(1280,720), new AspectRatio("16:9"), new AspectRatio("16:9"), new AspectRatio("1:1"), 921600),
                new DisplayResulation(@"720p (HDTV, Blu-ray)",new Size(1280,72), null, new AspectRatio("16:9"), null, 921600),
                new DisplayResulation(@"NeXT MegaPixel Display",new Size(1120,832), new AspectRatio("35:26"), new AspectRatio("4:3"), new AspectRatio("0.99"), 931840),
                new DisplayResulation(@"Wide XGA avg., BrightView (WXGA)",new Size(1280,768), new AspectRatio("5:3"), new AspectRatio("5:3"), new AspectRatio("1:1"), 983040),
                new DisplayResulation(@"Apple XGA (XGA+)",new Size(1152,864), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 995328),
                new DisplayResulation(@"Wide XGA max. (WXGA)",new Size(1280,800), new AspectRatio("8:5"), new AspectRatio("8:5"), new AspectRatio("1:1"), 1024000),
                new DisplayResulation(@"Sun-2 Prime Monochrome or Color Video, also common in Sun-3 and Sun-4 workstations",new Size(1152,900), new AspectRatio("32:25"), new AspectRatio("4:3"), new AspectRatio("1.041"), 1036800),
                new DisplayResulation(@"Network Computing Devices",new Size(1024,1024), new AspectRatio("1:1"), new AspectRatio("1:1"), new AspectRatio("1:1"), 1048576),
                new DisplayResulation(@"standardized HDTV 720p/1080i displays, 'HD ready', Used in many Windows 8 netbooks",new Size(1366,768), new AspectRatio("683:384"), new AspectRatio("16:9"), new AspectRatio("0.999"), 1049088),
                new DisplayResulation(@"Apple PowerBook G4",new Size(1280,854), new AspectRatio("640:427"), new AspectRatio("3:2"), new AspectRatio("1.001"), 1093120),
                new DisplayResulation(@"Sony VAIO P series",new Size(1600,768), new AspectRatio("25:12"), new AspectRatio("25:12"), new AspectRatio("1:1"), 1228800),
                new DisplayResulation(@"(SXGA−)",new Size(1280,960), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 1228800),
                new DisplayResulation(@"Wide SXGA or Wide XGA+ (WSXGA)",new Size(1440,900), new AspectRatio("8:5"), new AspectRatio("8:5"), new AspectRatio("1:1"), 1296000),
                new DisplayResulation(@"(SXGA)",new Size(1280,1024), new AspectRatio("5:4"), new AspectRatio("5:4"), new AspectRatio("1:1"), 1310720),
                new DisplayResulation(@"Apple PowerBook G4",new Size(1440,960), new AspectRatio("3:2"), new AspectRatio("3:2"), new AspectRatio("1:1"), 1382400),
                new DisplayResulation(@"Panasonic DVCPRO HD 1080, 59.94i",new Size(1280,1080), new AspectRatio("32:27"), new AspectRatio("16:9"), new AspectRatio("3:2"), 1382400),
                new DisplayResulation(@"900p (HD+)",new Size(1600,900), new AspectRatio("16:9"), new AspectRatio("16:9"), new AspectRatio("1:1"), 1440000),
                new DisplayResulation(@"(SXGA+)",new Size(1400,1050), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 1470000),
                new DisplayResulation(@"similar to A4 paper format",new Size(1440,1024), new AspectRatio("45:32"), new AspectRatio("7:5"), new AspectRatio("0.996"), 1474560),
                new DisplayResulation(@"HDV 1080i/1080p",new Size(1440,1080), new AspectRatio("4:3"), new AspectRatio("16:9"), new AspectRatio("4:3"), 1555200),
                new DisplayResulation(@"16CIF (16 * CIF)",new Size(1408,1152), new AspectRatio("1.22:1"), null, null, 1622016),
                new DisplayResulation(@"SGI 1600SW",new Size(1600,1024), new AspectRatio("25:16"), new AspectRatio("25:16"), new AspectRatio("1:1"), 1638400),
                new DisplayResulation(@"Digital Cinema 2K",new Size(2048,858), null, new AspectRatio("2.39:1"), null, 1757184),
                new DisplayResulation(@"(WSXGA+)",new Size(1680,1050), new AspectRatio("8:5"), new AspectRatio("8:5"), new AspectRatio("1:1"), 1764000),
                new DisplayResulation(@"(UXGA)",new Size(1600,1200), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 1920000),
                new DisplayResulation(@"HD 1080 (1080i, 1080p), FullHD (FHD)",new Size(1920,1080), new AspectRatio("16:9"), new AspectRatio("16:9"), new AspectRatio("1:1"), 2073600),
                new DisplayResulation(@"Digital Cinema 2K",new Size(1998,1080), null, new AspectRatio("1.85:1"), null, 2157840),
                new DisplayResulation(@"(WUXGA)",new Size(1920,1200), new AspectRatio("8:5"), new AspectRatio("8:5"), new AspectRatio("1:1"), 2304000),
                new DisplayResulation(@"2K (QWXGA)",new Size(2048,1152), new AspectRatio("16:9"), new AspectRatio("16:9"), new AspectRatio("1:1"), 2359296),
                new DisplayResulation(@"(supported by some GPUs, monitors, and games)",new Size(1792,1344), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 2408448),
                new DisplayResulation(@"Academy 2K",new Size(1828,1332), new AspectRatio("1.37:1"), new AspectRatio("1.37:1"), new AspectRatio("1:1"), 2434896),
                new DisplayResulation(@"(supported by some GPUs, monitors, and games)",new Size(1856,1392), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 2583552),
                new DisplayResulation(@"NEC CRV43, Ostendo CRVD, Alienware Curved Display (CWSXGA)",new Size(2880,900), new AspectRatio("16:5"), new AspectRatio("16:5"), new AspectRatio("1:1"), 2592000),
                new DisplayResulation(@"(supported by some GPUs, monitors, and games)",new Size(1800,1440), new AspectRatio("5:4"), new AspectRatio("5:4"), new AspectRatio("1:1"), 2592000),
                new DisplayResulation(@"Tesselar XGA (TXGA)",new Size(1920,1400), new AspectRatio("48:35"), new AspectRatio("7:5"), new AspectRatio("1.021"), 2688000),
                new DisplayResulation(@"Avielo Optix SuperWide 235 projector[15]",new Size(2538,1080), new AspectRatio("2.35:1"), new AspectRatio("2.35:1"), new AspectRatio("1.017"), 2741040),
                new DisplayResulation(@"Cinema TV from Philips and Vizio, Dell UltraSharp U2913WM, ASUS MX299Q, NEC EA294WMi, Philips 298X4QJAB, LG 29EA93, AOC Q2963PM",new Size(2560,1080), new AspectRatio("21:9"), new AspectRatio("21:9"), new AspectRatio("1:1"), 2764800),
                new DisplayResulation(@"(supported by some GPUs, monitors, and games)",new Size(1920,1440), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 2764800),
                new DisplayResulation(@"iPad (3rd Generation) QXGA",new Size(2048,1536), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 3145728),
                new DisplayResulation(@"Full Aperture Native 2K",new Size(2048,1556), new AspectRatio("1.316"), new AspectRatio("4:3"), new AspectRatio("~1:1"), 3186688),
                new DisplayResulation(@"(maximum resolution of the Sony GDM-FW900 and Hewlett Packard A7217A)",new Size(2304,1440), new AspectRatio("8:5"), new AspectRatio("8:5"), new AspectRatio("1:1"), 3317760),
                new DisplayResulation(@"Dell UltraSharp U2711, Dell XPS One 27, Apple iMac (QHD)",new Size(2560,1440), new AspectRatio("16:9"), new AspectRatio("16:9"), new AspectRatio("1:1"), 3686400),
                new DisplayResulation(@"(selectable on some displays and graphics cards)",new Size(2304,1728), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 3981312),
                new DisplayResulation(@"Dell Ultrasharp U3011, Dell 3007WFP, Dell 3008WFP, Gateway XHD3000, Samsung 305T, HP LP3065, HP ZR30W, Nexus 10 (WQXGA)",new Size(2560,1600), new AspectRatio("8:5"), new AspectRatio("8:5"), new AspectRatio("1:1"), 4096000),
                new DisplayResulation(@"Chromebook Pixel",new Size(2560,1700), new AspectRatio("128:85"), new AspectRatio("3:2"), new AspectRatio("0.996"), 4352000),
                new DisplayResulation(@"(max. CRT resolution. Supported by the Viewsonic P225f and some graphics cards)",new Size(2560,1920), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 4915200),
                new DisplayResulation(@"Apple 15inch MacBook Pro's Retina Display",new Size(2880,1800), new AspectRatio("8:5"), new AspectRatio("8:5"), new AspectRatio("1:1"), 5184000),
                new DisplayResulation(@"(QSXGA)",new Size(2560,2048), new AspectRatio("5:4"), new AspectRatio("5:4"), new AspectRatio("1:1"), 5242880),
                new DisplayResulation(@"HP Envy TouchSmart 14, Fujitsu Lifebook UH90/L, Lenovo Yoga 2 Pro (WQXGA+)",new Size(3200,1800), new AspectRatio("16:9"), new AspectRatio("16:9"), new AspectRatio("1:1"), 5760000),
                new DisplayResulation(@"(QSXGA+)",new Size(2800,2100), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 5880000),
                new DisplayResulation(@"(WQSXGA)",new Size(3200,2048), new AspectRatio("25:16"), new AspectRatio("25:16"), new AspectRatio("1:1"), 6553600),
                new DisplayResulation(@"Digital cinema 4K",new Size(4096,1714), null, new AspectRatio("2.39:1"), null, 7020544),
                new DisplayResulation(@"(QUXGA)",new Size(3200,2400), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 7680000),
                new DisplayResulation(@"2160p (4K UHD)",new Size(3840,2160), new AspectRatio("16:9"), new AspectRatio("16:9"), new AspectRatio("1:1"), 8294400),
                new DisplayResulation(@"Digital cinema 4K",new Size(3996,2160), null, new AspectRatio("1.85:1"), null, 8631360),
                new DisplayResulation(@"Digital Cinema Initiatives 4k (native resolution)",new Size(4096,2160), null, new AspectRatio("1.90:1"), null, 8847360),
                new DisplayResulation(@"(WQUXGA)",new Size(3840,2400), new AspectRatio("8:5"), new AspectRatio("8:5"), new AspectRatio("1:1"), 9216000),
                new DisplayResulation(@"(4K)",new Size(4096,2304), new AspectRatio("16:9"), new AspectRatio("16:9"), new AspectRatio("1:1"), 9437184),
                new DisplayResulation(@"Academy 4K",new Size(3656,2664), new AspectRatio("1.37:1"), new AspectRatio("1.37:1"), new AspectRatio("1:1"), 9739584),
                new DisplayResulation(@"(HXGA)",new Size(4096,3072), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 12582912),
                new DisplayResulation(@"Full Aperture 4K",new Size(4096,3112), new AspectRatio("1.316"), new AspectRatio("4:3"), new AspectRatio("~1:1"), 12746752),
                new DisplayResulation(@"(WHXGA)",new Size(5120,3200), new AspectRatio("8:5"), new AspectRatio("8:5"), new AspectRatio("1:1"), 16384000),
                new DisplayResulation(@"6K",new Size(6144,3160), null, new AspectRatio("1.94:1"), null, 19415040),
                new DisplayResulation(@"(HSXGA)",new Size(5120,4096), new AspectRatio("5:4"), new AspectRatio("5:4"), new AspectRatio("1:1"), 20971520),
                new DisplayResulation(@"IMAX Digital",new Size(5616,4096), null, new AspectRatio("1.37:1"), null, 23003136),
                new DisplayResulation(@"(WHSXGA)",new Size(6400,4096), new AspectRatio("25:16"), new AspectRatio("25:16"), new AspectRatio("1:1"), 26214400),
                new DisplayResulation(@"(HUXGA)",new Size(6400,4800), new AspectRatio("4:3"), new AspectRatio("4:3"), new AspectRatio("1:1"), 30720000),
                new DisplayResulation(@"4320p (8K UHD)",new Size(7680,4320), new AspectRatio("16:9"), new AspectRatio("16:9"), new AspectRatio("1:1"), 33177600),
                new DisplayResulation(@"(WHUXGA)",new Size(7680,4800), new AspectRatio("8:5"), new AspectRatio("8:5"), new AspectRatio("1:1"), 36864000),
                new DisplayResulation(@"(8K)",new Size(8192,4608), new AspectRatio("16:9"), new AspectRatio("16:9"), new AspectRatio("1:1"), 37748736),
                new DisplayResulation(@"Red Epic 617",new Size(28000,9334), null, new AspectRatio("3:1"), null, 261352000),
                new DisplayResulation(@"Betamax (NTSC)",new Size(480,320), null, new AspectRatio("4:3"), null, 120000),
                new DisplayResulation(@"Betamax Superbeta (NTSC)",new Size(480,380), null, new AspectRatio("4:3"), null, 136800),
                new DisplayResulation(@"Betamax (PAL/SECAM)",new Size(576,310), null, null, null, 144000),
                new DisplayResulation(@"VHS (NTSC)",new Size(480,320), null, new AspectRatio("4:3"), null, 153600),
                new DisplayResulation(@"Betamax Superbeta (PAL/SECAM)",new Size(576,370), null, null, null, 164160),
                new DisplayResulation(@"VHS (PAL/SECAM)",new Size(576,310), null, null, null, 178560),
                new DisplayResulation(@"S-VHS (NTSC)",new Size(480,530), null, new AspectRatio("4:3"), null, 192000),
                new DisplayResulation(@"NTSC",new Size(486,440), null, new AspectRatio("4:3"), null, 213840),
                new DisplayResulation(@"Undecoded PALplus",new Size(432,520), null, new AspectRatio("16:9"), null, 220000),
                new DisplayResulation(@"S-VHS (PAL/SECAM)",new Size(576,520), null, null, null, 230400),
                new DisplayResulation(@"Laserdisc (NTSC)",new Size(480,580), null, new AspectRatio("4:3"), null, 268800),
                new DisplayResulation(@"PAL, SECAM",new Size(576,520), null, new AspectRatio("4:3"), null, 299520),
                new DisplayResulation(@"PAL+",new Size(576,520), null, new AspectRatio("16:9"), null, 300000),
                new DisplayResulation(@"Laserdisc (PAL/SECAM)",new Size(576,570), null, null, null, 322560)
                #endregion
            };

        }
        public static List<DisplayResulation> getDisplayStandards(this Size size)
        {
            List<DisplayResulation> result = new List<DisplayResulation>();

            try
            {
                result = DisplayResulationStandards.Where(x => x.ResulationSize == size).ToList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
            finally
            {
                if (result.Count() == 0) 
                    result.Add(new DisplayResulation("This size is not Standard Display Resulation!", size, null, null, null, 0));
            }

            return result;
        }


        #region Filter Methods
        public static void SaltAndPepperNoise(this Bitmap image, double noiseAmount)
        {
            AForge.Imaging.Filters.SaltAndPepperNoise s = new AForge.Imaging.Filters.SaltAndPepperNoise();
            s.NoiseAmount = noiseAmount;
            s.ApplyInPlace(image);
        }

        public static void Negative(this Bitmap image)
        {
            AForge.Imaging.Filters.Invert filter = new AForge.Imaging.Filters.Invert();
            filter.ApplyInPlace(image);
        }

        public static void EuclideanColorFiltering(this Bitmap image)
        {
            AForge.Imaging.Filters.EuclideanColorFiltering filter = new AForge.Imaging.Filters.EuclideanColorFiltering();
            // set center colol and radius
            filter.CenterColor = Color.FromArgb(FilterColor.ToArgb());
            filter.Radius = (short)Range;
            filter.ApplyInPlace(image);
        }

        public static void ObjectDetection(ref Bitmap image)
        {
            Bitmap objectsImage = null;
            Bitmap mImage = (Bitmap)image.Clone();
            eulideanColorFilter.CenterColor = Color.FromArgb(FilterColor.ToArgb());
            eulideanColorFilter.Radius = (short)Range;

            objectsImage = image;
            eulideanColorFilter.ApplyInPlace(objectsImage);

            // lock image for further processing
            BitmapData objectsData = objectsImage.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
           
            // grayscaling
            UnmanagedImage grayImage = grayscaleFilter.Apply(new UnmanagedImage(objectsData));

            // unlock image
            objectsImage.UnlockBits(objectsData);

            // locate blobs 
            BlobCounterFilter.ProcessImage(grayImage);
            Rectangle[] rects = BlobCounterFilter.GetObjectRectangles();

            if (rects.Length > 0)
            {

                foreach (Rectangle objectRect in rects)
                {
                    // draw rectangle around derected object
                    Graphics g = Graphics.FromImage(mImage);
                    using (Pen pen = new Pen(Color.FromArgb(160, 255, 160), 5))
                    {
                        g.DrawRectangle(pen, objectRect);
                    }

                    g.Dispose();
                }


            }

            image = mImage;
        }

        public static void HistogramEqualization(this Bitmap image)
        {
            AForge.Imaging.Filters.HistogramEqualization filter = new AForge.Imaging.Filters.HistogramEqualization();
            filter.ApplyInPlace(image);
        }

        
        /// <summary>
        /// RGB2Gray converts RGB values to grayscale values by forming a weighted sum of the R, G, and B components:
        /// 0.2989 * R + 0.5870 * G + 0.1140 * B 
        /// Note that these are the same weights used by the rgb2ntsc function to compute the Y component. 
        /// </summary>
        public static void RGB2Gray(ref Bitmap image)
        {
            Bitmap grayImage = new Bitmap(image.Width, image.Height);
            
            //Grayscale gs = new Grayscale(0.2989, 0.5870, 0.1140);
            //image = gs.Apply(grayImage);
            
            for (int w = 0; w < image.Width; w++)
                for (int h = 0; h < image.Height; h++)
                {
                    int R = (int)(0.2989 * image.GetPixel(w, h).R);
                    int G = (int)(0.5870 * image.GetPixel(w, h).G);
                    int B = (int)(0.1140 * image.GetPixel(w, h).B);
                    grayImage.SetPixel(w, h, Color.FromArgb(R, G, B));
                }
            
            image = grayImage;
        }


        public static Bitmap parvaneh = new Bitmap(@"Parvaneh.jpg", true);
        public static void AddMoth(ref Bitmap image, Point Location)
        {
            Bitmap mothImage = (Bitmap)image.Clone();

            int width = Location.X - parvaneh.Width / 2;
            width = width < 0 ? 0 : width;
            width = width > mothImage.Width - parvaneh.Width ? mothImage.Width - parvaneh.Width : width;

            int height = Location.Y - parvaneh.Height / 2;
            height = height < 0 ? 0 : height;
            height = height > mothImage.Height - parvaneh.Height ? mothImage.Height - parvaneh.Height : height;

            for (int w = width; w < parvaneh.Width + width; w++)
                for (int h = height; h < parvaneh.Height + height; h++)
                {
                    Color px = parvaneh.GetPixel(w - width, h - height);
                    int LowLimit = 160;
                    int HightLimit = 255;
                    if (px.R > LowLimit && px.R < HightLimit && px.G > LowLimit && px.G < HightLimit && px.B > LowLimit && px.B < HightLimit)
                        continue;
                    mothImage.SetPixel(w, h, parvaneh.GetPixel(w - width, h - height));
                }

            image = mothImage;
        }
            

        public static Bitmap MotionAlarm(this Bitmap image)
        {
            return image;
        }
        #endregion

    }



    /// <summary>
    /// Get or define a display resulation standards.
    /// For the DisplayResulation class, 
    /// SAR (storage aspect ratio) is based solely on pixel count. 
    /// It does not take into account PAR (pixel aspect ratio, pixels may be non-square) and 
    /// thus the DAR (display aspect ratio, the aspect ratio of the actual image that is displayed) may differ.
    /// </summary>
    public struct DisplayResulation
    {
        /// <summary>
        /// Display Resulation Acronym
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Width and Height of Resulation Size
        /// </summary>
        public readonly Size ResulationSize;

        /// <summary>
        /// Storage Aspect Ratio
        /// </summary>
        public readonly AspectRatio SAR;

        /// <summary>
        /// Display Aspect Ratio
        /// </summary>
        public readonly AspectRatio DAR;

        /// <summary>
        /// Pixel Aspect Ratio
        /// </summary>
        public readonly AspectRatio PAR;

        /// <summary>
        /// Width × Height = (Counts of pixels)
        /// </summary>
        public readonly long Pixels;

        /// <summary>
        /// Get or define a display resulation standarts
        /// </summary>
        /// <param name="name">Display Resulation Standards Name or Acronym</param>
        /// <param name="size">Size of Resulation</param>
        /// <param name="sar">Storage Aspect Ratio</param>
        /// <param name="dar">Display Aspect Ratio</param>
        /// <param name="par">Pixel Aspect Ratio</param>
        /// <param name="pixels">Width*Height (Counts of Pixels)</param>
        public DisplayResulation(string name, Size size, AspectRatio sar, AspectRatio dar, AspectRatio par, long pixels)
        {
            this.Name = name;
            this.ResulationSize = size;
            this.SAR = sar;
            this.DAR = dar;
            this.PAR = par;
            this.Pixels = pixels;
        }
    }

    /// <summary>
    /// Stores and ordered pair of integers, typically width and height of a resulation aspect ratio.
    /// The aspect ratio of an image describes the proportional relationship between its width and its height.
    /// It is commonly expressed as two numbers separated by a colon, as in 16:9. For an x:y aspect ratio, 
    /// no matter how big or small the image is, 
    /// if the width is divided into x units of equal length and the height is measured using this same length unit, 
    /// the height will be measured to be y units. For example, 
    /// consider a group of images, all with an aspect ratio of 16:9. One image is 16 inches wide and 9 inches high.
    /// </summary>
    public class AspectRatio
    {
        public double WidthRatio;
        public double HeightRatio;

        public AspectRatio(double widthRatio, double heightRatio)
        {
            this.WidthRatio = widthRatio;
            this.HeightRatio = heightRatio;
        }
        public AspectRatio(string strAspectRatio)
        {
            try
            {
                int oh_Index = strAspectRatio.IndexOf(':');

                if (oh_Index > 0) // #:#
                {
                    Double.TryParse(strAspectRatio.Substring(0, strAspectRatio.IndexOf(':')), out WidthRatio);
                    Double.TryParse(strAspectRatio.Substring(strAspectRatio.IndexOf(':') + 1), out HeightRatio);
                }
                else // #:1 that ':' oh char is removed because Height is 1
                {
                    if (Double.TryParse(strAspectRatio, out this.WidthRatio))
                        this.HeightRatio = 1;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
        }
        public override string ToString() { return string.Format("{0}:{1}", WidthRatio, HeightRatio); }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is AspectRatio))
                return false;
            else
                return (WidthRatio == ((AspectRatio)obj).WidthRatio) && (HeightRatio == ((AspectRatio)obj).HeightRatio);
        }
        public override int GetHashCode() { return WidthRatio.GetHashCode() + HeightRatio.GetHashCode(); }
        public static bool operator ==(AspectRatio a, AspectRatio b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.WidthRatio == b.WidthRatio && a.HeightRatio == b.HeightRatio;
        }
        public static bool operator !=(AspectRatio a, AspectRatio b)
        {
            return !(a == b);
        }
    }
}