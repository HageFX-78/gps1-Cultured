VAR Rational = false
VAR Love = false
VAR Hope = false
VAR Acceptance = false

->Phase2

===Phase2===
Your Sister: You might sound like my brother, but I still have my doubts!!
Your Sister: Give me back my brother!! #Rational #Rational #Hope #Hope

    + I'm sorry that I didn't live up to your expectations, but.. I am still the same!
        ->PathA2
    + Stop it!! you already knew the whole truth!! Stop being delusional!
        ->PathB2
    + Come on... You have to accept the truth, you are dead!!
        ->PathC2
    + [*Show the remnant*] Remember when I made this for you?
        ->PathD2

//=============================================================PATH A===================================================
=PathA2
Your Sister: Things change, yes... 
Your Sister: But <b>YOU!!</b> you have the audacity to still lie?!? #Rational #Hope #Love #Acceptance
    + Come on... I'm not lying!! Listen to me!
        ~Rational=true
    + All I ever want is to have a strong trust between us.... That's really it.
        ~Hope=true
    + I am your brother, if you think I need to lie to you, then I'm saddened.
        ~Love=true
    + The sooner you accept that I am your brother, the easier time you will have.
        ~Acceptance=true
- ->checkA2

=checkA2
   {Rational||Hope: 
        Your Sister: *Sigh* Your persistence is annoying, you are giving me a headache!! #Love #Hope #Acceptance #Rational
        ~Rational = false
        ~Hope = false
        ->PathA3_1
    -else:
        Your Sister: Are you implying that I'm being a problematic child?? 
        Your Sister: Are you insulting me now?? #Acceptance #Love #Hope #Rational
        ~Love = false
        ~Acceptance = false
        ->PathA3_2
    }
    
=PathA3_1
    + That is really disappointing to hear...
        ~Love=true
    + Then let's end this charade of yours and I guarantee you'll be happier!!
        ~Hope=true
    + Maybe you are a goner... this place has corrupted you...
        ~Acceptance=true
    + If you feel that way, that means I must be on the right track!!
        ~Rational=true
- ->checkA3

=PathA3_2
    + If that is how you feel, then maybe..... this place has already corrupted you!!
        ~Acceptance=true
    + I am... lost for words..
        ~Love=true
    + Don't let your anger take control of you!! 
        ~Hope=true
    + All of these are your assumptions! Stop jumping to conclusions.
        ~Rational=true
    
- ->checkA3

=checkA3
   {Rational||Hope: 
        Your Sister: Maybe... maybe you are right... 
        Your Sister: No... <b>NO! I CAN'T BE THE ONE AT FAULT.</b> #Rational #Acceptance #Rational #Love
        ~Rational = false
        ~Hope = false
        ->PathA4_1
    -else:
        Your Sister: <b>Hah!</b> Your motivations are so <b>FEEBLE!!</b> I already feel better. #Rational #Hope #Acceptance #Love
        ~Love = false
        ~Acceptance = false
        ->PathA4_2
    }

=PathA4_1
    + It's okay to apologise if you have done something wrong.
        ~Rational=true
    + I'm.... I think I'm done trying to convince you any further.
        ~Acceptance=true
    + This is one step forward sis, let's resolve this...
        ~Rational=true
    + It will be easier if you can just own up to it!
        ~Love=true
        
- ->checkA4

=PathA4_2
    + You must realize this place is affecting your mind!
        ~Rational=true
    + What do you hope to gain by insulting me?!? I'm trying to help you!
        ~Hope=true
    + So that's it?? You are just going to accept this place as your final stay?
        ~Acceptance=true
    + Is your goal to piss me off? Is that just it?!?
        ~Love=true
    
- ->checkA4

=checkA4
   {Rational||Hope: 
        Your Sister: My brother would actually know what I want and how to make me happy... 
        Your Sister: ..... <b>THAT IS NOT YOU!</b> #Love #Acceptance #Rational #Hope
        ~Rational = false
        ~Hope = false
        ->PathA5_1
    -else:
        Your Sister: Thankfully, this will be the last time I'll talk to you. 
        Your Sister: What a waste of time and energy! #Acceptance #Love #Hope #Rational
        ~Love = false
        ~Acceptance = false
        ->PathA5_2
    }

=PathA5_1
    + Real or not, I still care for you no matter what.
        ~Love=true
    + Then you can keep waiting, I want to see how long you are willing wait.
        ~Acceptance=true
    + I may come out second best, but this is not over yet. I will convince you!
        ~Rational=true
    + I hope this brother of yours will help save you then.
        ~Hope=true

- ->checkA5

=PathA5_2
    + If that's how you feel, there is no point in trying to reach you.
        ~Acceptance=true
    + Disheartening.... that's how I would describe how i feel....
        ~Love=true
    + Why can't you be patient and have a little faith in me??
        ~Hope=true
    + You want to get out of this conversation? Why?
        ~Rational=true

- ->checkA5

=checkA5
   {Rational||Hope: 
        Your Sister: You will never understand what I've been through..... 
        Your Sister: Pain... Suffering... #Rational #Hope #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathA6_1
    -else:
        Your Sister: <i>That's great!!</i> It's about time you give up!! 
        Your Sister: Now... where is my real brother.. #Acceptance #Rational #Hope #Love
        ~Love = false
        ~Acceptance = false
        ->PathA6_2
    }

=PathA6_1
    + I don't know, but I want to... that's what being family is all about.
        ~Rational=true
    + Why not give me a chance... calm down and hopefully... You can be at ease.
        ~Hope=true
    + Find someone else to talk to you without losing their mind compared to me.
        ~Acceptance=true
    + No one else would have have the patience to continue, except me... trust me..
        ~Love=true
- ->checkA6

=PathA6_2
    + You can just rot here then, you are beyond saving.
        ~Acceptance=true
    + You should know already know I'm not giving up, that's how your brother operates.
        ~Rational=true
    + You will give in and give me a chance, I just know of it!
        ~Hope=true
    + You are truly pushing your luck, playing hard to get... 
        ~Love=true

- ->checkA6

=checkA6
Your sister seems to calm down, your words must have reached her
    + [Ask what she is thinking about] Do you really trust me now? Are you calm now?
    + [Ask how she feels] Sis? Are you okay?

- ->DONE


//=============================================================PATH B===================================================
=PathB2
Your Sister: Are you putting <b>ME</b> on the spot? 
Your Sister: My <b>real brother</b> would never do that!! #Rational #Hope #Love #Acceptance
    + This is a good time to realize that you are deluded.
        ~Rational=true  
    + I have to hope you can be better than this.
        ~Hope=true
    + Surely your loved ones educated you better than this.
        ~Love=true
    + If you face the truth, you will feel better. 
        ~Acceptance=true

- ->checkB2

=checkB2
    {Rational||Hope: 
        Your Sister: My brother would just accept me for who I am... Why can't you?
        Your Sister: <i>Oh?</i> That's because you are not!! #Rational #Hope #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathB3_1
    -else:
        Your Sister: So.. so many red flags... You are so self righteous... #Acceptance #Rational #Love #Hope
        ~Love = false
        ~Acceptance = false
        ->PathB3_2
    }
    
=PathB3_1
    + This reaction of yours... It is natural, but it's going to push me away...
        ~Rational=true
    + You are going to regret this.... I can assure you that.... 
        ~Hope=true
    + I have no reason to stop just because you say so.
        ~Acceptance=true
    + You are truly pushing your luck, even if I am your brother.
        ~Love=true
        
- ->checkB3

=PathB3_2
    + Okay... Let's assume you are right.. what do you lose if you give me a chance?
        ~Acceptance=true
    + You are just ignoring the truth to escape reality.
        ~Rational=true
    + Is this all a ruse to gain attention?
        ~Love=true
    + What do you gain by doing this?? What happens if you give me a chance?
        ~Hope=true
        
- ->checkB3

=checkB3
    {Rational||Hope: 
        Your Sister: You think you are slick with your words..
        Your Sister: You are just like the rest of them.. <b>A liar!!</b> #Love #Rational #Rational #Hope
        ~Rational = false
        ~Hope = false
        ->PathB4_1
    -else:
        Your Sister: Don't try to play me for a <b>fool!!</b>
        Your Sister: I wasn't born yesterday!! #Rational #Hope #Acceptance #Love
        ~Love = false
        ~Acceptance = false
        ->PathB4_2
    }

=PathB4_1
    + It's fine if you still think that way, I'm doing this for your sake.
        ~Love=true
    + I'm sure everyone has their own reasons, but you have to trust me.
        ~Rational=true
    + Why do you keep jumping to conclusions about who I am?
        ~Rational=true
    + Does that matter if I care about you? I won't give in!!
        ~Hope=true
    
- ->checkB4

=PathB4_2
    + I'm really not pretending to be your brother!! I know our history together!!
        ~Rational=true
    + If your hope is to drag this on and make me give up, you got another thing coming!
        ~Hope=true
    + You have to accept the truth.... I am your brother! That isn't going to change.
        ~Acceptance=true
    + I'm not trying to fool you!! I only want to help you out as your brother.
        ~Love=true
- ->checkB4

=checkB4
    {Rational||Hope: 
        Your Sister: If I keep this up, surely...
        Your Sister: Surely you will crack!! #Rational #Hope #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathB5_1
    -else:
        Your Sister: .....
        Your Sister: <b>THAT'S VERY EASY FOR YOU TO SAY.</b>
        Your Sister: You are only saying this to let my guard down!!! #Hope #Acceptance #Love #Rational
        ~Love = false
        ~Acceptance = false
        ->PathB5_2
    }

=PathB5_1
    + Why do you insist I am a fake, let alone crack??
        ~Rational=true
    + I would keep my hopes up if I were you, because I'm not going to crack!
        ~Hope=true
    + Would you prefer if I just admit and pretend that you are right? That am lying to you?
        ~Acceptance=true
    + You would love to see me give up. I'll be honest, I'm very close to doing it.
        ~Love=true
- ->checkB5

=PathB5_2
    + It only hurts because it is the truth!! can't you see that!!
        ~Hope=true
    + Would it make you feel better if I admit I was lying?!?? What do you want??
        ~Acceptance=true
    + You are getting more and more deluded!! 
        ~Love=true
    + Just your assumptions again. There is no proof of that.
        ~Rational=true
- ->checkB5

=checkB5
    {Rational||Hope: 
        Your Sister: Don't try to play mind games with me!! It won't work!
        Your Sister: You will never understand..... And I dont plan on trusting you!!
        Your Sister: I.... can't... #Rational #Hope #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathB6_1
    -else:
        Your Sister: <b>HAHAHAHA.</b>
        Your Sister: Seeing you lose your mind makes me really happy. #Acceptance #Rational #Hope #Love
        ~Love = false
        ~Acceptance = false
        ->PathB6_2
    }

=PathB6_1
    + I'm- ... Just trust me, give me a chance.. I'm willing to lend an ear to you.
        ~Rational=true
    + I just need one shot, deep down.. I know you hope I can save you from this.
        ~Hope=true
    + [*Accept the fact your sister is no longer sane*] I should accept the fact that you are beyond saving.
        ~Acceptance=true
    + You are seriously testing my patience here.
        ~Love=true
- ->checkB6

=PathB6_2
    + [*Accept reality and give up on her*] You are being manipulated by... whatever is going on in this place.
        ~Acceptance=true
    + Logically speaking, I've gotten this far, there is no reason to give up now.
        ~Rational=true
    + This isn't going to set me back, I'm saving you if it's the last thing I do!
        ~Hope=true
    + [If thats what you want...] I will give up and leave then....
        ~Love=true
- ->checkB6

=checkB6
Your sister seems to calm down, your words must have reached her
    + [Ask what she is thinking about] Do you really trust me now? Are you calm now?
    + [Ask how she feels] Sis? Are you okay?
    
- ->DONE

//=============================================================PATH C===================================================
=PathC2
Your Sister: <i>Nooo?</i> Mom and Dad are waiting for us at home? 
Your Sister: <i>Pfft, you must be crazy. </i> #Rational #Acceptance #Love #Hope

    + If they were watching right now, they would had said the same thing.
        ~Rational=true
    + Acceptance is hard, but it is what Mom and Dad would have wanted from their baby girl.
        ~Acceptance=true
    + How do you think Mom and Dad would react to this?
        ~Love=true
    + I bet if Mom was here, she would hope you face the truth.
        ~Hope=true
    
- ->checkC2

=checkC2
    {Rational||Hope: 
        Your Sister: <i>Anddd...?</i> How would <i><b>YOU</b></i> know that?? Did they tell you to say that??
        Your Sister: It's not very nice trying to <i>justify your lie~</i> #Rational #Hope #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathC3_1
    -else:
        Your Sister: Trying to gaslight <b>ME??</b>
        Your Sister: Not going to <i>worrrk~~~</i> #Rational #Rational #Acceptance #Love
        ~Love = false
        ~Acceptance = false
        ->PathC3_2
    }

=PathC3_1
    + You think I made that up? What do you take me for?
        ~Rational=true
    + I'd really hope if you would at least think about what I said.
        ~Hope=true
    + Surely you can snap out of this delusion and accept it...
        ~Acceptance=true
    + It's a shame what someone can say sometimes...
        ~Love=true
- ->checkC3
    
    
=PathC3_2
    + Why would you imply that I'm trying to manipulate you? 
        ~Rational=true
    + Avoiding what I said? Then I must be close to cracking you.
        ~Rational=true
    + I cannot accept that you are this delusion to see the truth.
        ~Acceptance=true
    + This is going nowhere. I don't know if my love for you can last.
        ~Love=true
- ->checkC3

=checkC3
    {Rational||Hope: 
        Your Sister: How can <b>YOU</b> speak for Mom and Dad???
        Your Sister: This is just disrespectful at this point. #Love #Acceptance #Rational #Hope
        ~Rational = false
        ~Hope = false
        ->PathC4_1
    -else:
        Your Sister: If Dad was here right now, he would be on <b>MY</b> side.
        Your Sister: You are a terrible <b>liar</b> and <b>a brother...</b> <i>If you really were one.</i> #Hope #Rational #Acceptance #Acceptance
        ~Love = false
        ~Acceptance = false
        ->PathC4_2
    }
    
=PathC4_1
    + The love and care I have for them is <b>insurmountable.</b>
        ~Love=true
    + I never said I was speaking for them. What are you talking about!
        ~Acceptance=true
    + That's a nice attempt at a strawman, but it isn't going to work!!
        ~Rational=true
    + I hope they will forgive me for overstepping my boundaries.
      But what I have to do now is to convince you.
        ~Hope=true
- ->checkC4

=PathC4_2
    + No one is perfect, I'm just trying my best to reach you.
        ~Hope=true
    + It's only natural that you'd reject what I say.
      But that is only going to cause more pain.
        ~Rational=true
    + I'm not throwing in the towel, even if what you say is true.
        ~Acceptance=true
    + The amount of distrust you shown in me... that hurts me.
        ~Acceptance=true
- ->checkC4

=checkC4
    {Rational||Hope: 
        Your Sister: You really annoy <b>ME</b> with your <i>"Everything is going to be alright"</i> attitude.
        Your Sister: Grow up my supposed ... <i>"brother"</i>. #Rational #Hope #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathC5_1
    -else:
        Your Sister: Try as you might, whatever you say is not going to work~
        Your Sister: You are just faking all of this from the start anyways. #Acceptance #Love #Hope #Rational
        ~Love = false
        ~Acceptance = false
        ->PathC5_2
    }
    
=PathC5_1
    + It is normal to have a positive outlook on things..
      What's not normal is being a constant debbie downer.
        ~Rational=true
    + I'm still the same old, same old. I really wish you would see that.
        ~Hope=true
    + The sooner you realize that what you are doing is not working, the better.
        ~Acceptance=true
    + I- .. Why would <i>that</i> annoy you? 
      I'm just doing my best to help you.
        ~Love=true
- ->checkC5

=PathC5_2
    + I'm starting to think otherwise if I should continue this conversation.
        ~Acceptance=true
    + It really pains me to hear you say that.
        ~Love=true
    + <i>For the 1000th time</i>, I just need you to give me a chance.
        ~Hope=true
    + Denial just shows that what I say is working.
        ~Rational=true
- ->checkC5

=checkC5
    {Rational||Hope: 
        Your Sister: <i>Man~</i> You are <i>seerriouslyy</i> beating a dead horse.
        Your Sister: Nevermind~ I want to see how much further you will go. #Rational #Hope #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathC6_1
    -else:
        Your Sister: Then you understand what it's like talking to you!!
        Your Sister: <b>HAHAHA</b> finally tasting your own medicine?!? #Acceptance #Love #Hope #Rational
        ~Love = false
        ~Acceptance = false
        ->PathC6_2
    }

=PathC6_1
    + I've made it this far, there is no reason for me to give up.
        ~Rational=true
    + You have to give up this scenario you made up in your head.
      I'm your brother, and thats that.
        ~Hope=true
    + Maybe you were doomed from the start.
      You can try to find someone else better than me. 
        ~Acceptance=true
    + [*My patience is being tested*] I'm slowly losing reasons to.
        ~Love=true
- ->checkC6

=PathC6_2
    + [*It's time I accept that she dont want to be saved*] Alright, I'll take a step back then.
        ~Acceptance=true
    + If giving you up is what it takes... Then... fine..
        ~Love=true
    + I have all the time in the world. I'll wait for you to let me explain myself.
        ~Hope=true
    + Why... Why not let me explain myself, and then see if you want to believe me??
        ~Rational=true
- ->checkC6

=checkC6
Your sister seems to calm down, your words must have reached her
    + [*Ask what she is thinking about*] Do you really trust me now? Are you calm now?
    + [*Ask how she feels*] Sis? Are you okay?
    
- ->DONE
//=============================================================PATH D===================================================
=PathD2
Your Sister: <i>Reaallyy?!?</i> <b>A teddy bear??</b>
Your Sister: That's the <b><i>best</i></b> you can do?? #Hope #Rational #Acceptance #Love
    + Think deeply, I'm sure you can remember it!!
        ~Hope=true
    + You didn't even give me a chance. Why?
        ~Rational=true
    + I brought something dear to you, you have to accept that and not be so stubborn.
        ~Acceptance=true
    + You loved this bear, what are you talking about!!
        ~Love=true
- ->checkD2

=checkD2
    {Rational||Hope: 
        Your Sister: I would be stupid to not have any doubts about what you say! #Acceptance #Hope #Love #Rational
        ~Rational = false
        ~Hope = false
        ->PathD3_1
    -else:
        Your Sister: <b>Hah!!</b> That’s hilarious!!
        Your Sister: You definitely lost it if you think a bear will do the trick. #Acceptance #Love #Rational #Hope
        ~Love = false
        ~Acceptance = false
        ->PathD3_2
    }
    
=PathD3_1
    + I'm not going to tolerate all of this. I can just walk away.
        ~Acceptance=true
    + [*Remind her about the past in hopes to convince her*] I made that bear when you got bullied at school.
        ~Hope=true
    + Every attempt I tried so far have just failed, you are really testing my patience here.
        ~Love=true
    + It would only make sense that I’m your brother, given that I know what this bear means to you!
        ~Rational=true
        
- ->checkD3

=PathD3_2
    + What makes you think I’m just going to sit here and let you take it out on me?
        ~Acceptance=true
    + Why do all my attempts to reach you have failed so far? 
        I’m getting exhausted
        ~Love=true
    + If I can prove to know about your connection with this bear, then I must be your brother.
        Correct?
        ~Rational=true
    + I can only hope you recognise the memories you had with this.
        ~Hope=true
- ->checkD3

=checkD3
    {Rational||Hope: 
        Your Sister: Your attempts are making me laugh, first teddy bear and now you are grasping at straws! 
        Your Sister: You’re such as joke. #Rational #Hope #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathD4_1
    -else:
        Your Sister: All I did was <i>taunt</i> and <i>tease</i> you a bit and you have already been <b>broken</b> to pieces!
        Your Sister: <i>So weaak, so predictable!!</i> #Acceptance #Love #Rational #Hope
        ~Love = false
        ~Acceptance = false
        ->PathD4_2
    }

=PathD4_1
    + That might be your view, but everything I do is for your sake.
        ~Rational=true
    + Even if you resent me, if there’s a chance to change you, I’ll take it.
        ~Hope=true
    + Why should I accept the way you are now?? 
        Insult after insult, and the lack of understanding.
        ~Acceptance=true
    + So you really don’t care anymore, maybe this place has consumed you.
        ~Love=true
        
- ->checkD4

=PathD4_2
    + Should I just give up and accept what you have become? Is that right to do?
        ~Acceptance=true
    + So you really don’t value our relationship anymore, maybe this place has consumed you.
        ~Love=true
    + What has been broken can be reforged, I’m trying my best to get out of this place.
        I’m sure you can too!
        ~Rational=true
    + I might be frustrated, but if I can get you to trust me again, then I can get out of this place.
        ~Hope=true
- ->checkD4

=checkD4
    {Rational||Hope: 
        Your Sister: Wow…. I knew your efforts were low but this...
        Your Sister: This is a new low!! #Hope #Rational #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathD5_1
    -else:
        Your Sister: This again…. You <i>reeaally</i> want to win at everything..
        Your Sister: Trying to <b>guilt trip</b> me? #Rational #Hope #Acceptance #Love
        ~Love = false
        ~Acceptance = false
        ->PathD5_2
    }
    
=PathD5_1
    + Even so, if just a little chance that I get to reach you, and leave this place, I’ll do it.
        ~Hope=true
    + Even if it's a low effort in your eyes, I'm still trying to prove my sincerity.
        ~Rational=true
    + That’s.... fair but I am still trying! 
        ~Acceptance=true
    + I- I don’t even know how to reply to that.
        ~Love=true
        
- ->checkD5

=PathD5_2
    + That’s <b><i>pure hyperbole</i></b>, I just want to talk, can you give me that chance??
        ~Rational=true
    + Let’s leave this place together, all I need is a second chance, we can do it .
        ~Hope=true
    + Where would I have gotten this doll then? 
        Should I admit that I made it?? I can’t even sew.
        ~Acceptance=true
    + What do you mean I’m not trying? Are you too deluded to realize anything??
        ~Love=true
- ->checkD5

=checkD5
    {Rational||Hope: 
        Your Sister: <i>Blabber all you want.</i> 
        Your Sister: I can’t possibly take someone who doesn't understand me seriously. #Rational #Hope #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathD6_1
    -else:
        Your Sister: Seeing you get frustrated is really soothing for me. 
        Your Sister: <i>A little longer and you will return to me.</i> #Acceptance #Rational #Hope #Love
        ~Love = false
        ~Acceptance = false
        ->PathD6_2
    }
    
=PathD6_1
    + If you just give me the time, then I’ll be able to. Let me set things straight.
        ~Rational=true
    + A chance is all I need, I hope that's not too much to ask.
        ~Hope=true
    + [*Choose to accept you and your sister’s fate*] All right, I’ll take a step back and give you some space.
        ~Acceptance=true
    + If loving you means that I have to let you go....
        <b>Then, go.</b>
        ~Love=true
        
- ->checkD6

=PathD6_2
    + [*Give up on your sister*] I should just give up on you....
        ~Acceptance=true
    + You obviously know I won’t give in!! I’ll do whatever it takes!
        ~Rational=true
    + I won’t go anywhere. I'm staying here till you come to me! I know you will.
        ~Hope=true
    + [*Rethink about your love for your sister*] Maybe it’s not worth saving you.
        ~Love=true
- ->checkD6

=checkD6
Your sister seems to calm down, your words must have reached her
    + [Ask what she is thinking about] Do you really trust me now? Are you calm now?
    + [Ask how she feels] Sis? Are you okay?
    
- ->DONE
    
->END