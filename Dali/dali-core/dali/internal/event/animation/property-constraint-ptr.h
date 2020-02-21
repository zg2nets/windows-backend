#ifndef DALI_INTERNAL_PROPERTY_CONSTRAINT_PTR_H
#define DALI_INTERNAL_PROPERTY_CONSTRAINT_PTR_H

/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

// INTERNAL INCLUDES
#include <dali/internal/event/animation/property-constraint.h>
#include <dali/internal/common/owner-pointer.h>

namespace Dali
{

namespace Internal
{

template <class P>
struct PropertyConstraintPtr
{
  typedef OwnerPointer< PropertyConstraint<P> > Type;
};

} // namespace Internal

} // namespace Dali

#endif // DALI_INTERNAL_PROPERTY_CONSTRAINT_PTR_H
