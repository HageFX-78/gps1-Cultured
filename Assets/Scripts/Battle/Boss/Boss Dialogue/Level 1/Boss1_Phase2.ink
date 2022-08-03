VAR Rational = false
VAR Love = false
VAR Hope = false
VAR Acceptance = false

->Phase2

===Phase2===
Boss: .....
Boss: If you think I would trust you with thes- these simple words, you are gravely mistaken!!
Boss: You might sound like my brother, but I still have my doubts!!
Boss: Give me back my brother!!

    + I'm sorry that I didn't live up to your expectations, but.. I am still the same!
        ->PathA2
        ~Rational=true
    + Stop it!! you already knew the whole truth!! Stop being delusional
        ->PathB2
        ~Rational=true
    + Come on now... You have to accept the truth, you are dead!!
        ->PathC2
        ~Hope=true
    + [*Show the remnant*] Remember when I made this for you?
        ->PathD2
        ~Hope=true
        
//=============================================================PATH A===================================================
=PathA2
Boss: Things change, yes... 
Boss: But you!! you have the audacity to still lie?!?
    + Come on... I'm not lying!! Listen to me!
        ~Rational=true
    + All I ever want is to have a strong trust between us.... That's really it
        ~Hope=true
    + I am your brother, if you think I need to lie to you, then I'm saddened
        ~Love=true
    + The sooner you accept that I am your brother, the easier time you will have
        ~Acceptance=true
- ->checkA2

=checkA2
   {Rational||Hope: 
        Boss: *Sigh* Your persistence is annoying, you are giving me a headache!!
        ~Rational = false
        ~Hope = false
        ->PathA3_1
    -else:
        Boss: Are you implying that I'm being a problematic child?? Are you insulting me now??
        ~Love = false
        ~Acceptance = false
        ->PathA3_2
    }
    
=PathA3_1
    + That is really disappointing to hear.. 
        ~Love=true
    + Then let's end this charade of yours and I guarantee you'll be happier!!
        ~Hope=true
    + Maybe you are a goner... this place has corrupted you...
        ~Acceptance=true
    + If you feel that way, that means I must be on the right track!!
        ~Rational=true
- ->checkA4

=PathA3_2
    + If that is how you feel, then maybe..... this place has already corrupted you!!
        ~Acceptance=true
    + I am... lost... for words..
        ~Love=true
    + Don't let your anger take control of you!! 
        ~Hope=true
    + All of these are your assumptions! Stop jumping to conclusions
        ~Rational=true
    
- ->checkA4

=checkA4
   {Rational||Hope: 
        Boss: Maybe... maybe you are right... 
        Boss: No... NO! I CAN'T BE THE ONE AT FAULT
        ~Rational = false
        ~Hope = false
        ->PathA4_1
    -else:
        Boss: Hah! Your motivations are so FEEBLE!! I already feel better.
        ~Love = false
        ~Acceptance = false
        ->PathA4_2
    }

=PathA4_1
    + It's okay to apologise if you have done something wrong
        ~Rational=true
    + I'm.... I think I'm done trying to convince you any further
        ~Acceptance=true
    + This is one step forward sis, let's resolve this...
        ~Rational=true
    + It will be easier if you can just own up to it!
        ~Love=true
        
- ->checkA5

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

=checkA5
   {Rational||Hope: 
        Boss: My brother would actually know what I want and how to make me happy... 
        Boss: .....THAT IS NOT YOU!
        ~Rational = false
        ~Hope = false
        ->PathA5_1
    -else:
        Boss: Thankfully, this will be the last time I'll talk to you. 
        Boss: What a waste of time and energy!
        ~Love = false
        ~Acceptance = false
        ->PathA5_2
    }

=PathA5_1
    + Real or not, I still care for you no matter what
        ~Love=true
    + Then you can keep waiting, I want to see how long you are willing wait
        ~Acceptance=true
    + I may come out second best, but this is not over yet. I will convince you!
        ~Rational=true
    + I hope this brother of yours will help save you then
        ~Hope=true

- ->checkA6

=PathA5_2
    + If that's how you feel, there is no point in trying to reach you.
        ~Acceptance=true
    + Disheartening.... that's how I would describe how i feel....
        ~Love=true
    + Why can't you be patient and have a little faith in me??
        ~Hope=true
    + You want to get out of this conversation? Why?
        ~Rational=true

- ->checkA6

=checkA6
   {Rational||Hope: 
        Boss: You will never understand what I've been through..... 
        Boss: Pain... Suffering...
        ~Rational = false
        ~Hope = false
        ->PathA6_1
    -else:
        Boss: That's great!! It's about time you give up!! 
        Boss: Now... where is my real brother..
        ~Love = false
        ~Acceptance = false
        ->PathA6_2
    }

=PathA6_1
    + I don't know, but I want to... that's what being family is all about
        ~Rational=true
    + Why not give me a chance... calm down and hopefully... You can be at ease
        ~Hope=true
    + Find someone else to talk to you without losing their mind compared to me
        ~Acceptance=true
    + No one else would have have the patience to continue, except me... trust me..
        ~Love=true
- ->checkA7

=PathA6_2
    + You can just rot here then, you are beyond saving
        ~Acceptance=true
    + You should know already know I'm not giving up, that's how your brother operates
        ~Rational=true
    + You will give in and give me a chance, I just know of it!
        ~Hope=true
    + You are truly pushing your luck, playing hard to get... 
        ~Love=true

- ->checkA7

=checkA7
Boss: *She seems to calm down for real time this time...*
    + [Ask what she is thinking about] Do you really trust me now? Are you calm now?
    + [Ask how she feels] Sis? Are you okay?

- ->DONE



//=============================================================PATH B===================================================
=PathB2
Boss: What is this?!? Are you putting me on the spot? 
Boss: My real brother would never do that!!
    + This is a good time to realize that you are deluded
        ~Rational=true  
    + I have to hope you can be better than this
        ~Hope=true
    + Surely your loved ones educated you better than this
        ~Love=true
    + If you face the truth, you will feel better. 
        ~Acceptance=true

- ->checkB2

=checkB2
    {Rational||Hope: 
        Boss: My brother would just accept me for who I am... Why can't you?
        Boss: Oh? That's because you are not!!
        ~Rational = false
        ~Hope = false
        ->PathB3_1
    -else:
        Boss: So.. so many red flags... You are so self righteous...
        ~Love = false
        ~Acceptance = false
        ->PathB3_2
    }
    
=PathB3_1
    + This reaction of yours... It is natural, but it's going to push me away...
        ~Rational=true
    + You are going to regret this.... I can assure you that.... 
        ~Hope=true
    + I have no reason to stop just because you say so
        ~Acceptance=true
    + You are truly pushing your luck, even if I am your brother
        ~Love=true
        
- ->checkB3

=PathB3_2
    + Okay... Let's assume you are right.. what do you lose if you give me a chance?
        ~Acceptance=true
    + You are just ignoring the truth to escape reality
        ~Rational=true
    + Is this all a ruse to gain attention?
        ~Love=true
    + What do you gain by doing this?? What happens if you give me a chance?
        ~Hope=true
        
- ->checkB3

=checkB3
    {Rational||Hope: 
        Boss: You think you are slick with your words..
        Boss: You are just like the rest of them.. A liar!!
        ~Rational = false
        ~Hope = false
        ->PathB4_1
    -else:
        Boss: Don't try to play me for a fool!!
        Boss: I wasn't born yesterday!!
        ~Love = false
        ~Acceptance = false
        ->PathB4_2
    }

=PathB4_1
    + It's fine if you still think that way, I'm doing this for your sake
        ~Love=true
    + I'm sure everyone has their own reasons, but you have to trust me
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
    + You have to accept the truth.... I am your brother! That isn't going to change
        ~Acceptance=true
    + I'm not trying to fool you!! I only want to help you out as your brother
        ~Love=true
- ->checkB4

=checkB4
    {Rational||Hope: 
        Boss: If I keep this up, surely...
        Boss: Surely you will crack!!
        ~Rational = false
        ~Hope = false
        ->PathB5_1
    -else:
        Boss: .....
        Boss: THAT'S VERY EASY FOR YOU TO SAY
        Boss: You are only saying things to let my guard down
        ~Love = false
        ~Acceptance = false
        ->PathB5_2
    }

=PathB5_1
    + .
        ~Rational=true
    + .
        ~Hope=true
    + .
        ~Acceptance=true
    + .
        ~Love=true
- ->checkB5

=PathB5_2
    + .
        ~Rational=true
    + .
        ~Hope=true
    + .
        ~Acceptance=true
    + .
        ~Love=true
- ->checkB5

=checkB5
    {Rational||Hope: 
        Boss: 
        Boss: 
        ~Rational = false
        ~Hope = false
        ->DONE
    -else:
        Boss: 
        ~Love = false
        ~Acceptance = false
        ->DONE
    }







//=============================================================PATH C===================================================
=PathC2
No


- ->DONE

//=============================================================PATH D===================================================
=PathD2

- ->DONE
->END