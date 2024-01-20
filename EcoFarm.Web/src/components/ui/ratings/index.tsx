import { range } from "lodash-es";

interface iProps {
    rating: number;
}
export const StarRating = (props: iProps) => {
    const maxStars = 5; // Change this if you want a different number of stars
  
    return (
      <div className="flex items-center">
        {range(maxStars).map((_, index) => (
          <svg
            key={index}
            className={`h-6 w-6 fill-current text-${index < props.rating ? 'yellow-500' : 'gray-300'}`}
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
          >
            <path
              d="M12 2c-.1 0-.3 0-.4.1L8 8l-7.9 1.2c-.4.1-.7.5-.8.9s0 .8.2 1l5.7 5.6L5.3 21c-.1.4.1.8.4 1s.7.3 1 .2l6.6-3.5 6.6 3.5c.2.1.4.1.6.1.3 0 .6-.1.8-.4s.3-.6.2-1l-1.3-7.6 5.7-5.6c.3-.3.3-.7.2-1s-.5-.7-.9-.8L16 8l-3.5-5.9c-.1-.4-.5-.7-.9-.9s-.8-.2-1.2-.1z"
            />
          </svg>
        ))}
      </div>
    );
  };