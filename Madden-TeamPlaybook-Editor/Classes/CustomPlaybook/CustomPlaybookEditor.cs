using System;
using System.Collections.Generic;

namespace Madden20CustomPlaybookEditor
{
    [Serializable]
    public class CustomPlaybookFormation
    {
        public List<CustomPlaybookSubFormation> SubFormations { get; set; }
        public CPFM CPFM { get; set; }
        public PBFM PBFM { get; set; }
    }

    [Serializable]
    public class CustomPlaybookSubFormation
    {
        public List<CustomPlaybookPLAY> Plays { get; set; }
        public List<SETL> SETL { get; set; }
        public List<PGPL> PGPL { get; set; }
        public List<SPKF> SPKF { get; set; }
        public List<SPKG> SPKG { get; set; }
        public List<SETP> SETP { get; set; }
        public List<SGFF> SGFF { get; set; }
        public List<SETG> SETG { get; set; }
    }

    [Serializable]
    public class CustomPlaybookPLAY
    {
        public List<SETL> SETL { get; set; }
        public List<PBPL> PBPL { get; set; }
        public List<PGPL> PGPL { get; set; }
        public List<PLPD> PLPD { get; set; }
        public List<PLRD> PLRD { get; set; }
        public List<PLYS> PLYS { get; set; }
        public List<PBAI> PBAI { get; set; }
        public List<PBAU> PBAU { get; set; }
        public List<PLCM> PLCM { get; set; }
        public List<PPCT> PPCT { get; set; }
        public List<SDEF> SDEF { get; set; }
        public List<SETP> SETP { get; set; }
        public List<SETG> SETG { get; set; }
        public List<SGFF> SGFF { get; set; }
        public List<SPKF> SPKF { get; set; }
        public List<SPKG> SPKG { get; set; }
        public List<SRFT> SRFT { get; set; }
        public List<List<PSAL>> PSAL { get; set; }
        public List<ARTL> ARTL { get; set; }
    }

    [Serializable]
    public class CPFM
    {
        public int rec { get; set; }
        public int FORM { get; set; }
        public int FTYP { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   FORM: " + FORM +
                "   FTYP: " + FTYP +
                "   Name: " + name;
        }
    }

    [Serializable]
    public class PBFM
    {
        public int rec { get; set; }
        public int FAU1 { get; set; }
        public int FAU2 { get; set; }
        public int FAU3 { get; set; }
        public int FAU4 { get; set; }
        public int pbfm { get; set; }
        public int FTYP { get; set; }
        public int ord_ { get; set; }
        public int grid { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   FAU1: " + FAU1 + "\t" +
                "   FAU2: " + FAU2 + "\t" +
                "   FAU3: " + FAU3 + "\t" +
                "   FAU4: " + FAU4 + "\t" +
                "   pbfm: " + pbfm + "\t" +
                "   FTYP: " + FTYP + "\t" +
                "   ord_: " + ord_ + "\t" +
                "   grid: " + grid + "\t" +
                "   Name: " + name;
        }
    }

    [Serializable]
    public class SGFF
    {
        public int rec { get; set; }
        public int SETL { get; set; }
        public int SGF_ { get; set; }
        public string name { get; set; }
        public int dflt { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETL: " + SETL +
                "   SGF_: " + SGF_ +
                "   Name: " + name +
                "   dflt: " + dflt;
        }
    }

    [Serializable]
    public class ARTL : IEquatable<ARTL>
    {
        public override string ToString()
        {
            return
                "rec: " + rec +
                "   artl: " + artl +
                "   acnt: " + acnt + "\n" +

                "   sp: " + "[" + string.Join(", ", sp) + "]\n" +
                "   ls: " + "[" + string.Join(", ", ls) + "]\n" +
                "   at: " + "[" + string.Join(", ", at) + "]\n" +
                "   ct: " + "[" + string.Join(", ", ct) + "]\n" +
                "   lt: " + "[" + string.Join(", ", lt) + "]\n" +
                "   au: " + "[" + string.Join(", ", au) + "]\n" +
                "   av: " + "[" + string.Join(", ", av) + "]\n" +
                "   ax: " + "[" + string.Join(", ", ax) + "]\n" +
                "   ay: " + "[" + string.Join(", ", ay) + "]";
        }

        public int rec { get; set; }
        public int artl { get; set; }
        public int acnt { get; set; }

        public int[] sp { get; set; }
        public int[] ls { get; set; }
        public int[] at { get; set; }
        public int[] ct { get; set; }
        public int[] lt { get; set; }
        public int[] au { get; set; }
        public int[] av { get; set; }
        public int[] ax { get; set; }
        public int[] ay { get; set; }

        public bool Equals(ARTL other)
        {
            //Check whether the compared object is null. 
            if (ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (
                this.artl == other.artl &&
                this.acnt == other.acnt &&
                this.sp == other.sp &&
                this.ls == other.ls &&
                this.at == other.at &&
                this.ct == other.ct &&
                this.lt == other.lt &&
                this.au == other.au &&
                this.av == other.av &&
                this.ax == other.ax &&
                this.ay == other.ay
                ) return true;

            //Check whether the PSAL IDs are equal. 
            return artl.Equals(other.artl);
        }

        // If Equals() returns true for a pair of objects then GetHashCode() must return the same value for these objects. 

        public override int GetHashCode()
        {
            //Get hash code for the Code field. 
            int hashARTLID = artl.GetHashCode();

            //Calculate the hash code for the product. 
            return hashARTLID;
        }
    }

    [Serializable]
    public class PSAL : IEquatable<PSAL>
    {
        public int rec { get; set; }
        public int val1 { get; set; }
        public int val2 { get; set; }
        public int val3 { get; set; }
        public int psal { get; set; }
        public int code { get; set; }
        public int step { get; set; }
        public int poso { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   val1: " + val1 +
                "   val2: " + val2 +
                "   val3: " + val3 +
                "   psal: " + psal +
                "   code: " + code +
                "   step: " + step +
                "   poso: " + poso;
        }

        public bool Equals(PSAL other)
        {
            //Check whether the compared object is null. 
            if (ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (
                this.psal == other.psal &&
                this.val1 == other.val1 &&
                this.val2 == other.val2 &&
                this.val3 == other.val3
                )
                return true;

            //Check whether the PSAL IDs are equal. 
            return psal.Equals(other.psal);
        }

        // If Equals() returns true for a pair of objects then GetHashCode() must return the same value for these objects. 

        public override int GetHashCode()
        {
            //Get hash code for the Code field. 
            int hashPSALID = psal.GetHashCode();

            //Calculate the hash code for the product. 
            return hashPSALID;
        }

        public PSAL Clone()
        {
            return new PSAL
            {
                rec = rec,
                val1 = val1,
                val2 = val2,
                val3 = val3,
                psal = psal,
                code = code,
                step = step
            };
        }
    }

    [Serializable]
    public class SETL
    {
        public int rec { get; set; }
        public int setl { get; set; }
        public int FORM { get; set; }
        public int MOTN { get; set; }
        public int CLAS { get; set; }
        public int SETT { get; set; }
        public int SITT { get; set; }
        public int SLF_ { get; set; }
        public string name { get; set; }
        public int poso { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETL: " + setl +
                "   FORM: " + FORM +
                "   MOTN: " + MOTN +
                "   CLAS: " + CLAS +
                "   SETT: " + SETT +
                "   SITT: " + SITT +
                "   SLF_: " + SLF_ +
                "   Name: " + name +
                "   poso: " + poso;
        }

        public SETL Clone()
        {
            return new SETL
            {
                rec = rec,
                setl = setl,
                FORM = FORM,
                MOTN = MOTN,
                CLAS = CLAS,
                SETT = SETT,
                SITT = SITT,
                SLF_ = SLF_,
                name = name,
                poso = poso
            };
        }
    }

    [Serializable]
    public class PGPL
    {
        public int rec { get; set; }
        public int BOKL { get; set; }
        public int SETL { get; set; }
        public int PLYL { get; set; }
        public int PBST { get; set; }
        public int PLYT { get; set; }
        public int ord_ { get; set; }
        public int Flag { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   BOKL: " + BOKL +
                "   SETL: " + SETL +
                "   PLYL: " + PLYL +
                "   PBST: " + PBST +
                "   PLYT: " + PLYT +
                "   ord_: " + ord_ +
                "   Flag: " + Flag;
        }
    }

    [Serializable]
    public class PBPL
    {
        public int rec { get; set; }
        public int COMF { get; set; }
        public int SETL { get; set; }
        public int PLYL { get; set; }
        public int SRMM { get; set; }
        public int SITT { get; set; }
        public int PLYT { get; set; }
        public int PLF_ { get; set; }
        public string name { get; set; }
        public int risk { get; set; }
        public int motn { get; set; }
        public int vpos { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   COMF: " + COMF +
                "   SETL: " + SETL +
                "   PLYL: " + PLYL +
                "   SRMM: " + SRMM +
                "   SITT: " + SITT +
                "   PLYT: " + PLYT +
                "   PLF_: " + PLF_ +
                "   Name: " + name +
                "   risk: " + risk +
                "   motn: " + motn +
                "   vpos: " + vpos;
        }

        public PBPL DeepCopy()
        {
            PBPL other = (PBPL)MemberwiseClone();
            return other;
        }

        public PBPL Clone()
        {
            return new PBPL
            {
                rec = rec,
                COMF = COMF,
                SETL = SETL,
                PLYL = PLYL,
                SRMM = SRMM,
                SITT = SITT,
                PLYT = PLYT,
                PLF_ = PLF_,
                name = name,
                risk = risk,
                motn = motn,
                vpos = vpos
            };
        }
    }

    [Serializable]
    public class PLPD
    {
        public int rec { get; set; }
        public int com1 { get; set; }
        public int con1 { get; set; }
        public int per1 { get; set; }
        public int rcv1 { get; set; }
        public int com2 { get; set; }
        public int con2 { get; set; }
        public int per2 { get; set; }
        public int rcv2 { get; set; }
        public int com3 { get; set; }
        public int con3 { get; set; }
        public int per3 { get; set; }
        public int rcv3 { get; set; }
        public int com4 { get; set; }
        public int con4 { get; set; }
        public int per4 { get; set; }
        public int rcv4 { get; set; }
        public int com5 { get; set; }
        public int con5 { get; set; }
        public int per5 { get; set; }
        public int rcv5 { get; set; }
        public int PLYL { get; set; }

        public PLPD DeepCopy()
        {
            PLPD other = (PLPD)MemberwiseClone();
            return other;
        }

        public PLPD Clone()
        {
            return new PLPD
            {
                rec = rec,
                com1 = com1,
                con1 = con1,
                per1 = per1,
                rcv1 = rcv1,
                com2 = com2,
                con2 = con2,
                per2 = per2,
                rcv2 = rcv2,
                com3 = com3,
                con3 = con3,
                per3 = per3,
                rcv3 = rcv3,
                com4 = com4,
                con4 = con4,
                per4 = per4,
                rcv4 = rcv4,
                com5 = com5,
                con5 = con5,
                per5 = per5,
                rcv5 = rcv5,
                PLYL = PLYL
            };
        }
    }

    [Serializable]
    public class PLRD
    {
        public int rec { get; set; }
        public int PLYL { get; set; }
        public int hole { get; set; }

        public PLRD DeepCopy()
        {
            PLRD other = (PLRD)MemberwiseClone();
            return other;
        }

        public PLRD Clone()
        {
            return new PLRD
            {
                rec = rec,
                PLYL = PLYL,
                hole = hole
            };
        }
    }

    [Serializable]
    public class PLYS : IEquatable<PLYS>
    {
        public int rec { get; set; }
        public string Position { get; set; }
        public int PSAL { get; set; }
        public int ARTL { get; set; }
        public int PLYL { get; set; }
        public int PLRR { get; set; }
        public int poso { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   Position: " + Position +
                "   PSAL: " + PSAL +
                "   ARTL: " + ARTL +
                "   PLYL: " + PLYL +
                "   PLRR: " + PLRR +
                "   poso: " + poso;
        }

        public bool Equals(PLYS other)
        {
            //Check whether the compared object is null. 
            if (ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (ReferenceEquals(this, other)) return true;

            //Check whether the PSAL IDs are equal. 
            return PSAL.Equals(other.PSAL);
        }

        // If Equals() returns true for a pair of objects then GetHashCode() must return the same value for these objects. 

        public override int GetHashCode()
        {
            //Get hash code for the Code field. 
            int hashPSALID = PSAL.GetHashCode();

            //Calculate the hash code for the product. 
            return hashPSALID;
        }

        public PLYS DeepCopy()
        {
            PLYS other = (PLYS)MemberwiseClone();
            return other;
        }

        public PLYS Clone()
        {
            return new PLYS
            {
                rec = rec,
                Position = Position,
                PSAL = PSAL,
                ARTL = ARTL,
                PLYL = PLYL,
                PLRR = PLRR,
                poso = poso
            };
        }
    }

    [Serializable]
    public class PBAI
    {
        public int rec { get; set; }
        public int BOKL { get; set; }
        public int PLYL { get; set; }
        public int AIGR { get; set; }
        public int prct { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   BOKL: " + BOKL +
                "   PLYL: " + PLYL +
                "   AIGR: " + AIGR +
                "   prct: " + prct;
        }
    }

    [Serializable]
    public class PBAU
    {
        public int rec { get; set; }
        public int BOKL { get; set; }
        public int PBPL { get; set; }
        public int FTYP { get; set; }
        public int pbau { get; set; }
        public int Flag { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   BOKL: " + BOKL +
                "   PBPL: " + PBPL +
                "   FTYP: " + FTYP +
                "   PBAU: " + pbau +
                "   Flag: " + Flag;
        }
    }

    [Serializable]
    public class PLCM
    {
        public int rec { get; set; }
        public int per1 { get; set; }
        public int dir1 { get; set; }
        public int ply1 { get; set; }
        public int per2 { get; set; }
        public int dir2 { get; set; }
        public int ply2 { get; set; }
        public int per3 { get; set; }
        public int dir3 { get; set; }
        public int ply3 { get; set; }
        public int per4 { get; set; }
        public int dir4 { get; set; }
        public int ply4 { get; set; }
        public int per5 { get; set; }
        public int dir5 { get; set; }
        public int ply5 { get; set; }
        public int PLYL { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   per1: " + per1 +
                "   dir1: " + dir1 +
                "   ply1: " + ply1 +
                "   per2: " + per2 +
                "   dir2: " + dir2 +
                "   ply2: " + ply2 +
                "   per3: " + per3 +
                "   dir3: " + dir3 +
                "   ply3: " + ply3 +
                "   per4: " + per4 +
                "   dir4: " + dir4 +
                "   ply4: " + ply4 +
                "   per5: " + per5 +
                "   dir5: " + dir5 +
                "   ply5: " + ply5 +
                "   PLYL: " + PLYL;
        }
    }

    [Serializable]
    public class PPCT
    {
        public int rec { get; set; }
        public int PLYL { get; set; }
        public int conp { get; set; }
        public int recr { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   PLYL: " + PLYL +
                "   conp: " + conp +
                "   recr: " + recr;
        }
    }

    [Serializable]
    public class SDEF
    {
        public int rec { get; set; }
        public int ATCA { get; set; }
        public int PLYL { get; set; }
        public int DFLP { get; set; }
        public int STRP { get; set; }
        public int STRR { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   ATCA: " + ATCA +
                "   PLYL: " + PLYL +
                "   DFLP: " + DFLP +
                "   STRP: " + STRP +
                "   STRR: " + STRR;
        }
    }

    [Serializable]
    public class SRFT
    {
        public int rec { get; set; }
        public int SIDE { get; set; }
        public int YOFF { get; set; }
        public int TECH { get; set; }
        public int PLYL { get; set; }
        public int STAN { get; set; }
        public int PLYR { get; set; }
        public int PRIS { get; set; }
        public int GAPS { get; set; }
        public int ASSS { get; set; }
        public int PRIW { get; set; }
        public int GAPW { get; set; }
        public int ASSW { get; set; }

        public override string ToString()
        {
            return
                "rec: " + rec +
                "   SIDE: " + SIDE +
                "   YOFF: " + YOFF +
                "   TECH: " + TECH +
                "   PLYL: " + PLYL +
                "   STAN: " + STAN +
                "   PLYR: " + PLYR +
                "   PRIS: " + PRIS +
                "   GAPS: " + GAPS +
                "   ASSS: " + ASSS +
                "   PRIW: " + PRIW +
                "   GAPW: " + GAPW +
                "   ASSW: " + ASSW;
        }

        public SRFT Clone()
        {
            return new SRFT
            {
                rec = rec,
                SIDE = SIDE,
                YOFF = YOFF,
                TECH = TECH,
                PLYL = PLYL,
                STAN = STAN,
                PLYR = PLYR,
                PRIS = PRIS,
                GAPS = GAPS,
                ASSS = ASSS,
                PRIW = PRIW,
                GAPW = GAPW,
                ASSW = ASSW
            };
        }
    }

    [Serializable]
    public class SETP
    {
        public int rec { get; set; }
        public int SETL { get; set; }
        public int setp { get; set; }
        public int SGT_ { get; set; }
        public int arti { get; set; }
        public int opnm { get; set; }
        public int tabo { get; set; }
        public int poso { get; set; }
        public int odep { get; set; }
        public int flas { get; set; }
        public int DPos { get; set; }
        public int EPos { get; set; }
        public int fmtx { get; set; }
        public int artx { get; set; }
        public int fmty { get; set; }
        public int arty { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETL: " + SETL +
                "   setp: " + setp +
                "   SGT_: " + SGT_ +
                "   arti: " + arti +
                "   opnm: " + opnm +
                "   tabo: " + tabo +
                "   poso: " + poso +
                "   odep: " + odep +
                "   flas: " + flas +
                "   DPos: " + DPos +
                "   EPos: " + EPos +
                "   fmtx: " + fmtx +
                "   artx: " + artx +
                "   fmty: " + fmty +
                "   arty: " + arty;
        }

        public SETP DeepCopy()
        {
            SETP other = (SETP)MemberwiseClone();
            return other;
        }

        public SETP Clone()
        {
            return new SETP
            {
                rec = rec,
                SETL = SETL,
                setp = setp,
                SGT_ = SGT_,
                arti = arti,
                opnm = opnm,
                tabo = tabo,
                poso = poso,
                odep = odep,
                flas = flas,
                DPos = DPos,
                EPos = EPos,
                fmtx = fmtx,
                artx = artx,
                fmty = fmty,
                arty = arty
            };
        }
    }

    [Serializable]
    public class SETG
    {
        public int rec { get; set; }
        public int SETP { get; set; }
        public int SGF_ { get; set; }
        public int SF__ { get; set; }
        public float x___ { get; set; }
        public float y___ { get; set; }
        public float fx__ { get; set; }
        public float fy__ { get; set; }
        public int anm_ { get; set; }
        public int dir_ { get; set; }
        public int fanm { get; set; }
        public int fdir { get; set; }
        public bool active { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETP: " + SETP +
                "   SGF_: " + SGF_ +
                "   SF__: " + SF__ +
                "   x___: " + x___ +
                "   y___: " + y___ +
                "   fx__: " + fx__ +
                "   fy__: " + fy__ +
                "   anm_: " + anm_ +
                "   dir_: " + dir_ +
                "   fanm: " + fanm +
                "   fdir: " + fdir +
                "   active: " + active;
        }

        public SETG DeepCopy()
        {
            SETG other = (SETG)MemberwiseClone();
            return other;
        }

        public SETG Clone()
        {
            return new SETG
            {
                rec = rec,
                SETP = SETP,
                SGF_ = SGF_,
                SF__ = SF__,
                x___ = x___,
                y___ = y___,
                fx__ = fx__,
                fy__ = fy__,
                anm_ = anm_,
                dir_ = dir_,
                fanm = fanm,
                fdir = fdir,
                active = active
            };
        }
    }

    [Serializable]
    public class SPKF
    {
        public int rec { get; set; }
        public int SETL { get; set; }
        public int SPF_ { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   SETL: " + SETL + "\t" +
                "   SPF_: " + SPF_ + "\t" +
                "   name: " + name;
        }
    }

    [Serializable]
    public class SPKG
    {
        public int rec { get; set; }
        public int SPF_ { get; set; }
        public int poso { get; set; }
        public int DPos { get; set; }
        public int EPos { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   SPF_: " + SPF_ + "\t" +
                "   poso: " + poso + "\t" +
                "   DPos: " + DPos + "\t" +
                "   EPos: " + EPos;
        }
    }
}
