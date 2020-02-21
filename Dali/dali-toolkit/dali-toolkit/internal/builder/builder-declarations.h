#ifndef DALI_TOOLKIT_BUILDER_DECLARATIONS_H
#define DALI_TOOLKIT_BUILDER_DECLARATIONS_H

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

// EXTERNAL INCLUDES
#include <dali/public-api/common/extents.h>
#include <dali/public-api/math/rect.h>
#include <dali/public-api/math/vector2.h>
#include <dali/public-api/math/vector3.h>
#include <dali/public-api/math/vector4.h>
#include <dali/public-api/math/matrix.h>
#include <dali/public-api/math/matrix3.h>

// INTERNAL INCLUDES
#include <dali-toolkit/devel-api/builder/tree-node.h>
#include <dali-toolkit/internal/builder/optional-value.h>

typedef Dali::Toolkit::TreeNode TreeNode;
typedef TreeNode::ConstIterator TreeConstIter;

typedef OptionalValue<const TreeNode&> OptionalChild;
typedef OptionalValue<std::string> OptionalString;
typedef OptionalValue<float> OptionalFloat;
typedef OptionalValue<int> OptionalInteger;
typedef OptionalValue<unsigned int> OptionalUnsignedInt;
typedef OptionalValue<bool> OptionalBoolean;
typedef OptionalValue<Dali::Vector2> OptionalVector2;
typedef OptionalValue<Dali::Vector3> OptionalVector3;
typedef OptionalValue<Dali::Vector4> OptionalVector4;
typedef OptionalValue<std::string> OptionalString;
typedef OptionalValue<Dali::Matrix> OptionalMatrix;
typedef OptionalValue<Dali::Matrix3> OptionalMatrix3;
typedef OptionalValue<Dali::Rect<int> > OptionalRect;
typedef OptionalValue<Dali::Extents> OptionalExtents;

#endif // DALI_TOOLKIT_BUILDER_DECLARATIONS_H
