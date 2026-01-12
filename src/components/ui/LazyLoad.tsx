import { type ReactNode } from 'react';
import { useIntersectionObserver } from '@/hooks/use-intersection-observer';
import { cn } from '@/lib/utils';

interface LazyLoadProps {
  children: ReactNode;
  className?: string;
  minHeight?: string | number;
  threshold?: number;
  rootMargin?: string;
}

/**
 * LazyLoad component that defers rendering of its children until they are near the viewport.
 *
 * Uses IntersectionObserver to detect visibility.
 * Prevents unnecessary API calls and heavy DOM rendering on initial load.
 *
 * @param minHeight - Minimum height to reserve for the component to prevent layout shift.
 * @param rootMargin - Margin around the root to start loading before the element is visible (default: 200px).
 */
export function LazyLoad({
  children,
  className,
  minHeight = '100px',
  threshold = 0,
  rootMargin = '200px',
}: LazyLoadProps) {
  const { ref, isIntersecting } = useIntersectionObserver({
    threshold,
    rootMargin,
    triggerOnce: true,
  });

  return (
    <div
      ref={ref}
      className={cn('w-full', className)}
      style={{ minHeight }}
    >
      {isIntersecting ? children : null}
    </div>
  );
}
