#import <AVFoundation/AVFoundation.h>

@interface AudioSessionCategory : NSObject
+ (void)setPlayback;
@end

@implementation AudioSessionCategory

+ (void)setPlayback {
    AVAudioSession* audioSession = [AVAudioSession sharedInstance];
    [audioSession setCategory:AVAudioSessionCategoryPlayback error:nil];
}

@end


#ifdef __cplusplus
extern "C" {
#endif

void __setAudioSessionCategoryPlayback() {
    return [AudioSessionCategory setPlayback];
}

#ifdef __cplusplus
}
#endif