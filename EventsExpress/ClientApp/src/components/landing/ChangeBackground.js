import * as React from 'react';
import ReactDOM from 'react-dom';


//const randomImages = [
//    "'url(https://c.pxhere.com/photos/da/5a/silhouette_in_xinjiang_ghost_city_people_sunset_joke_together-454504.jpg!d)'",
//    "'url(https://overdrivestudiosdotes.files.wordpress.com/2014/09/people-concert-wallpaper-1080p-backgrounds-concert-people-background-25952.jpg)'"
//];

//export default function ChangeBackground() {
//    const [value, setValue] = React.useState(0);

//    React.useState(() => {
//        const interval = setInterval(() => {
//            setValue((v) => (v === randomImages.length - 1 ? 0 : v + 1));
//        }, 10000);
//    }, []);

//    return (
//        <div id="background" style={{ backgroundImage: randomImages[value] }}> </div>
//        );
//}

//const colors = ["#0088FE", "#00C49F", "#FFBB28"];
//const delay = 2500;

//function Slideshow() {
//    const [index, setIndex] = React.useState(0);
//    const timeoutRef = React.useRef(null);

//    function resetTimeout() {
//        if (timeoutRef.current) {
//            clearTimeout(timeoutRef.current);
//        }
//    }

//    React.useEffect(() => {
//        resetTimeout();
//        timeoutRef.current = setTimeout(
//            () =>
//                setIndex((prevIndex) =>
//                    prevIndex === colors.length - 1 ? 0 : prevIndex + 1
//                ),
//            delay
//        );

//        return () => {
//            resetTimeout();
//        };
//    }, [index]);

//    return (
//        <div className="slideshow">
//            <div
//                className="slideshowSlider"
//                style={{ transform: `translate3d(${-index * 100}%, 0, 0)` }}
//            >
//                {colors.map((backgroundColor, index) => (
//                    <div
//                        className="slide"
//                        key={index}
//                        style={{ backgroundColor }}
//                    ></div>
//                ))}
//            </div>

//            <div className="slideshowDots">
//                {colors.map((_, idx) => (
//                    <div
//                        key={idx}
//                        className={`slideshowDot${index === idx ? " active" : ""}`}
//                        onClick={() => {
//                            setIndex(idx);
//                        }}
//                    ></div>
//                ))}
//            </div>
//        </div>
//    );
//}

//ReactDOM.render(<Slideshow />, document.getElementById("App"));
